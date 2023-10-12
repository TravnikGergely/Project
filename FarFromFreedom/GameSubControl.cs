using FarFromFreedom.Logic;
using FarFromFreedom.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FarFromFreedom
{
    public class GameSubControl
    {
        IGameLogic? logic;
        MediaPlayer sound = new MediaPlayer();
        MediaPlayer mainSound = new MediaPlayer();
        public DispatcherTimer? gameTimer;
        DispatcherTimer? EventTimer;
        DispatcherTimer pauseTimer;


        private int counterTimer = 0;
        private int counterHitTimer = 0;

        private BaseControl? baseControl;
        private IGameModel model;
        private bool initializeChecker = false;
        private bool playing = true;

        List<Key> pressedKeys = new List<Key>();
        List<Key> keysThatMatters = new List<Key>()
            { Key.W, Key.S, Key.A, Key.D, Key.Up, Key.Down,Key.Right,
            Key.Left, Key.Space, Key.Enter, Key.P, Key.Escape, Key.H, Key.T };

        public void Dispose()
        {
            //this.logic = null;
            this.gameTimer?.Stop();
            this.gameTimer = null;
            this.EventTimer?.Stop();
            this.EventTimer = null;
            this.pauseTimer?.Stop();
            this.pauseTimer = null;
        }

        public void LogicNull()
        {
            this.logic = null;
        }

        public void Init(IGameModel model, BaseControl baseControl)
        {
            if (logic is null)
            {
                logic = new GameLogic(model);
            }
            this.model = model;
            this.model.PauseModel = null;

            this.baseControl = baseControl;
            Window win = Window.GetWindow(baseControl);
            if (win != null && initializeChecker == false)
            {
                mainSound.Open(new Uri(Path.Combine("StoryVideo", "music.mp3"), UriKind.Relative));
                mainSound.Volume = 0.4;

                mainSound.Play();

                gameTimer = new DispatcherTimer();
                EventTimer = new DispatcherTimer();
                this.pauseTimer = new DispatcherTimer();

                gameTimer.Interval = TimeSpan.FromMilliseconds(30);
                EventTimer.Interval = TimeSpan.FromSeconds(0.5);
                pauseTimer.Interval = TimeSpan.FromMilliseconds(10);

                gameTimer.Tick += this.EnemyMove;
                gameTimer.Tick += this.EnemyHit;
                gameTimer.Tick += this.BulletMove;
                gameTimer.Tick += this.EnemyDamaged;
                gameTimer.Tick += this.EnemyDestroy;
                gameTimer.Tick += this.ItemPickedUp;
                gameTimer.Tick += this.GameEnded;
                gameTimer.Tick += this.DoorGenerator;
                gameTimer.Tick += this.TearDestroyer;
                gameTimer.Tick += this.DoorEnter;

                gameTimer.Tick += this.MainCharacterMove;
                gameTimer.Tick += this.TestButtons;
                gameTimer.Tick += this.MainCharacterShoot;
                gameTimer.Tick += this.MusicPlayer;
                gameTimer.Tick += this.EscapePress;
                gameTimer.Tick += this.WonCheck;

                EventTimer.Tick += EventCounterTimer;

                pauseTimer.Tick += PauseMenuInputHandler;

                gameTimer.Start();
                EventTimer.Start();

                //win.KeyDown += this.MainCharacterMove;
                //win.KeyDown += TestButtons;
                //win.KeyDown += this.MainCharacterShoot;
                //win.KeyDown += MusicPlayer;
                //win.KeyDown += EscapePress;
                win.KeyDown += PutKeyToList;
                win.KeyUp += RemoveKeyFromList;
                initializeChecker = true;
            }
        }

        private void WonCheck(object? sender, EventArgs e)
        {
            if (this.logic.Won)
            {
                this.sound.Stop();
                this.mainSound.Stop();
                this.EventTimer.Stop();
                this.gameTimer.Stop();
                this.pauseTimer.Start();
                this.model.Won = true;

            }
        }

        private void PauseMenuInputHandler(object? sender, EventArgs e)
        {
            if (this.model.Won)
            {
                if (this.pressedKeys.Contains(Key.Enter) || this.pressedKeys.Contains(Key.Space))
                {
                    this.pressedKeys.Remove(Key.Enter);
                    this.pressedKeys.Remove(Key.Space);
                    FarFromFreedom.SaveHighscore saveWin = new SaveHighscore(this.logic);

                    Window original = Window.GetWindow(this.baseControl);
                    original.WindowStartupLocation = WindowStartupLocation.Manual;

                    saveWin.Width = 200;
                    saveWin.Height = 200;
                    saveWin.WindowStartupLocation = WindowStartupLocation.Manual;
                    saveWin.Left = original.Left + (original.Width / 2) - (saveWin.Width / 2);
                    saveWin.Top = original.Top + (original.Height / 2) - (saveWin.Height / 2);
                    saveWin.ShowDialog();
                    this.model.Won = false;
                    this.model.PauseModel = null;

                    this.initializeChecker = false;
                    this.baseControl.ChangeModel(new MenuModel());
                }
                return;
            }

            if (this.model.PauseModel == null) { return; }

            if (this.model.PauseModel.IsDead)
            {
                if (this.pressedKeys.Contains(Key.Enter) || this.pressedKeys.Contains(Key.Space))
                {
                    this.pressedKeys.Remove(Key.Enter);
                    this.pressedKeys.Remove(Key.Space);
                    FarFromFreedom.SaveHighscore saveWin = new SaveHighscore(this.logic);

                    Window original = Window.GetWindow(this.baseControl);
                    original.WindowStartupLocation = WindowStartupLocation.Manual;

                    saveWin.Width = 200;
                    saveWin.Height = 200;
                    saveWin.WindowStartupLocation = WindowStartupLocation.Manual;
                    saveWin.Left = original.Left + (original.Width / 2) - (saveWin.Width / 2);
                    saveWin.Top = original.Top + (original.Height / 2) - (saveWin.Height / 2);
                    saveWin.ShowDialog();
                    this.model.PauseModel.IsDead = false;
                    this.model.PauseModel = null;
                    this.model.Won = false;
                    this.initializeChecker = false;
                    this.baseControl.ChangeModel(new MenuModel());
                }
                return;
            }

            if (this.pressedKeys.Contains(Key.Down))
            {
                if (this.model.PauseModel?.SelectedIndex != 1)
                {
                    this.model.PauseModel.SelectedIndex = 1;
                    this.model.PauseModel.SaveOpacity = 1;
                    this.model.PauseModel.ContinueOpacity = 0.7;
                }
                this.pressedKeys.Remove(Key.Down);
            }
            else if (pressedKeys.Contains(Key.Up))
            {
                if (this.model.PauseModel?.SelectedIndex != 0)
                {
                    this.model.PauseModel.SelectedIndex = 0;
                    this.model.PauseModel.SaveOpacity = 0.7;
                    this.model.PauseModel.ContinueOpacity = 1;
                }
                this.pressedKeys.Remove(Key.Up);
            }
            else if (pressedKeys.Contains(Key.Escape))
            {
                this.model.PauseModel = null;
                this.pressedKeys.Remove(Key.Escape);
                this.gameTimer.Start();
                this.EventTimer.Start();
            }
            else if (pressedKeys.Contains(Key.Enter))
            {
                if (this.model.PauseModel?.SelectedIndex == 0)
                {
                    this.model.PauseModel = null;
                    this.pressedKeys.Remove(Key.Escape);
                    this.gameTimer.Start();
                    this.EventTimer.Start();
                }
                else
                {
                    this.sound.Stop();
                    this.mainSound.Stop();
                    this.logic.GameSave();
                    this.initializeChecker = false;
                    this.baseControl.ChangeModel(new MenuModel());
                }
            }
        }

        private void SaveHighscore()
        {
            this.logic.GameSave();
            SaveHighscore saveWin = new SaveHighscore(this.logic);
            Window original = Window.GetWindow(this.baseControl);
            original.WindowStartupLocation = WindowStartupLocation.Manual;

            saveWin.Width = 200;
            saveWin.Height = 200;
            saveWin.WindowStartupLocation = WindowStartupLocation.Manual;
            saveWin.Left = original.Left + (original.Width / 2) - (saveWin.Width / 2);
            saveWin.Top = original.Top + (original.Height / 2) - (saveWin.Height / 2);
            saveWin.ShowDialog();

            this.initializeChecker = false;
            this.baseControl.ChangeModel(new MenuModel());
            //this.logic.SaveHighscore();
        }

        private void RemoveKeyFromList(object sender, KeyEventArgs e)
        {
            if (this.keysThatMatters.Contains(e.Key) && this.pressedKeys.Contains(e.Key))
            {
                this.pressedKeys.Remove(e.Key);
            }
        }

        private void PutKeyToList(object sender, KeyEventArgs e)
        {
            if (this.keysThatMatters.Contains(e.Key) && !this.pressedKeys.Contains(e.Key))
            {
                this.pressedKeys.Add(e.Key);
            }
        }

        private void EscapePress(object? sender, EventArgs e)
        {
            if (this.pressedKeys.Contains(Key.Escape))
            {
                bool toPause = this.model.PauseModel == null ? true : false;
                if (toPause)
                {
                    this.pauseTimer.Start();
                    this.gameTimer?.Stop();
                }
                else
                {
                    this.pauseTimer.Stop();
                    this.gameTimer?.Start();
                }

                this.logic?.Pause(toPause);
                this.pressedKeys.Remove(Key.Escape);
            }
        }

        private void MusicPlayer(object? sender, EventArgs e)
        {
            if (this.pressedKeys.Contains(Key.P))
            {
                this.pressedKeys.Remove(Key.P);
                if (playing)
                {
                    mainSound.Pause();
                }
                else
                {
                    mainSound.Play();
                }
                playing = !playing;
            }
        }

        private void TearDestroyer(object? sender, EventArgs e)
        {
            logic?.DisposeOutOFBoundsTears();
        }

        private void TestButtons(object? sender, EventArgs e)
        {
            if (this.pressedKeys.Contains(Key.H))
            {
                this.pressedKeys.Remove(Key.H);
                MessageBox.Show("X: " + this.model.Character.Area.Rect.X + "\nY: " + this.model.Character.Area.Rect.Y);
            }
            if (this.pressedKeys.Contains(Key.T))
            {
                this.pressedKeys.Remove(Key.T);
                logic.GameSave();
            }
        }

        private void DoorEnter(object? sender, EventArgs e)
        {
            int? roomid = logic?.DoorIntersect();
            if (roomid != -1 && roomid != null)
            {

                baseControl.ChangeModel(logic?.ChangeRoom((int)roomid));
                //MessageBox.Show(roomid.ToString());
            }
        }

        private void DoorGenerator(object? sender, EventArgs e)
        {
            logic?.GenerateDoors();
        }

        private void MainCharacterMove(object? sender, EventArgs e)
        {
            if (this.pressedKeys.Contains(Key.Up))
            {
                logic?.PLayerMove(Key.Up);
            }
            else if (this.pressedKeys.Contains(Key.Down))
            {
                logic?.PLayerMove(Key.Down);
            }
            if (this.pressedKeys.Contains(Key.Left))
            {
                logic?.PLayerMove(Key.Left);
            }
            else if (this.pressedKeys.Contains(Key.Right))
            {
                logic?.PLayerMove(Key.Right);
            }
        }

        private void MainCharacterShoot(object? sender, EventArgs e)
        {
            int w = this.pressedKeys.Contains(Key.W) ? this.pressedKeys.IndexOf(Key.W) : 99999;
            int a = this.pressedKeys.Contains(Key.A) ? this.pressedKeys.IndexOf(Key.A) : 99999;
            int s = this.pressedKeys.Contains(Key.S) ? this.pressedKeys.IndexOf(Key.S) : 99999;
            int d = this.pressedKeys.Contains(Key.D) ? this.pressedKeys.IndexOf(Key.D) : 99999;
            int min = (new int[4] { w, a, s, d }).Min();
            if (counterTimer >= 2 && min != 99999)
            {
                counterTimer = (int)logic?.PlayerShoot(this.pressedKeys[min], counterTimer);
            }
        }

        public void RoomLoad(object sender, EventArgs e)
        {
            if (true)
            {
                this.logic?.RoomUp();
            }
            else
            {
                this.logic?.RoomDown();
            }
        }

        private void EnemyDestroy(object sender, EventArgs e)
        {
            this.logic?.EnemyDestroy();
        }

        private void EventCounterTimer(object sender, EventArgs e)
        {
            this.counterTimer++;
            this.counterHitTimer++;
            if (this.model.PauseModel != null)
            {
                this.baseControl.InvalidateVisual();
            }
        }

        private void EnemyDamaged(object sender, EventArgs e)
        {
            this.logic?.EnemyDamaged();
        }

        private void ItemPickedUp(object sender, EventArgs e)
        {
            this.logic?.ItemPicked();
        }

        private void EnemyHit(object sender, EventArgs e)
        {
            if (counterHitTimer >= 2)
            {
                if (this.logic.EnemyHit())
                {
                    if (playing)
                    {
                        Random r = new Random();
                        sound.Open(new Uri(Path.Combine("StoryVideo", $"Hurt_grunt_{r.Next(0, 3)}.wav"), UriKind.Relative));
                        sound.Play();
                    }
                    counterHitTimer = 0;
                }
            }
        }

        private void BulletMove(object sender, EventArgs e)
        {
            this.logic?.BulletMove();
        }

        private void GameEnded(object sender, EventArgs e)
        {
            if ((bool)this.logic?.GameEnd())
            {
                this.model.PauseModel = new PauseModel();
                this.model.PauseModel.IsDead = true;

                this.gameTimer.Stop();
                this.EventTimer.Stop();
                this.pauseTimer.Start();

                if (playing)
                {
                    sound.Open(new Uri(Path.Combine("StoryVideo", "Gobby_dies_new.wav"), UriKind.Relative));
                    sound.Play();
                }
                mainSound.Pause();

            }
        }

        private void EnemyMove(object sender, EventArgs e)
        {
            this.logic?.EnemyMove();
        }
    }
}
