using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using FarFromFreedom.Model.Items;
using FarFromFreedom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FarFromFreedom.Logic
{
    public class GameLogic : IGameLogic
    {
        public int CurrentRoom => currentRoom;
        public int CurrentLevel => currentLevel;
        public bool Won => this.won;

        private IGameModel gameModel;
        private IFarFromFreedomRepository farFromFreedomRepository;
        private int currentRoom = -1;
        private int currentLevel = 0;
        private bool won = false;

        public GameLogic(IGameModel gameModel)
        {
            this.gameModel = gameModel;
            farFromFreedomRepository = FarFromFreedomRepository.Instance();
        }
        public GameLogic(int levels, string fileName)
        {
            farFromFreedomRepository = FarFromFreedomRepository.Instance();
        }

        public GameLogic()
        {

        }

        public void EnemyMove()
        {
            MainCharacter character = (MainCharacter)gameModel.Character;
            List<RoomDecorationItem> roomDecorationItems = new List<RoomDecorationItem>();
            Enemy enemy;
            Queue<Enemy> queue = new Queue<Enemy>();
            foreach (Enemy enemyAdd in gameModel.Enemies)
            {
                queue.Enqueue(enemyAdd);
            }
            foreach (var item in gameModel.Items)
            {
                if (item.Description == "RoomItem")
                {
                    roomDecorationItems.Add((RoomDecorationItem)item);
                }
            }

            while (queue.Count > 0)
            {
                enemy = queue.Dequeue();
                Direction direction = new Direction();
                bool enemyItemIsCollision = false;

                double x = character.Area.Rect.X - enemy.Area.Rect.X;
                double y = character.Area.Rect.Y - enemy.Area.Rect.Y;

                if (x > 0 && y > 0)
                {
                    direction = Direction.UpRight;
                }
                else if (x > 0 && y == 0)
                {
                    direction = Direction.Right;
                }
                else if (x == 0 && y < 0)
                {
                    direction = Direction.Up;
                }
                else if (x < 0 && y > 0)
                {
                    direction = Direction.UpLeft;
                }
                else if (x < 0 && y == 0)
                {
                    direction = Direction.Left;
                }
                else if (x < 0 && y < 0)
                {
                    direction = Direction.DownLeft;
                }
                else if (x == 0 && y > 0)
                {
                    direction = Direction.Down;
                }
                else if (x > 0 && y < 0)
                {
                    direction = Direction.DownRight;
                }
                else
                {
                    continue;
                }

                EnemyMover(enemy, direction);
            }
        }

        public bool EnemyIsCollision(Queue<Enemy> queue, Enemy enemy)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                Enemy enemyCollision = queue.Dequeue();
                if (enemyCollision.IsCollision(enemy))
                {
                    enemyCollision.MoveUpLeft();
                    enemy.MoveDownRight();
                    return true;
                }
                else
                {
                    queue.Enqueue(enemyCollision);
                }
            }
            return false;
        }

        public void EnemyDestroy()
        {
            Enemy removableEnemy = null;
            foreach (Enemy enemy in gameModel.Enemies)
            {
                if (enemy.CurrentHealth <= 0)
                {
                    removableEnemy = enemy;
                }
            }
            if (removableEnemy != null)
            {
                ItemAdder(gameModel, removableEnemy.Area.Rect);
                gameModel.Enemies.Remove(removableEnemy);
                gameModel.Character.HighscoreUp(removableEnemy.Highscore);
            }
        }

        public void EnemyDamaged()
        {
            Bullet removeableBullet = null;
            List<int> indexes = new List<int>();

            foreach (Enemy enemy in gameModel.Enemies)
            {
                foreach (var bullet in gameModel.Bullets)
                {
                    bool isCollision = bullet.IsCollision(enemy);
                    if (isCollision)
                    {
                        enemy.CurrentHealthDown(gameModel.Character.Power);
                        removeableBullet = bullet;
                    }
                }
                if (removeableBullet != null)
                {
                    gameModel.Bullets.Remove(removeableBullet);
                    removeableBullet = null;
                }
            }
        }

        public bool EnemyHit()
        {
            bool result = false;
            foreach (Enemy enemy in gameModel.Enemies)
            {
                bool isCollision = enemy.IsCollision((MainCharacter)gameModel.Character);
                if (isCollision)
                {
                    gameModel.Character.CurrentHealthDown(enemy.Power);
                    result = true;
                }
            }
            return result;
        }

        public void GameSave(string fileName)
        {
            this.farFromFreedomRepository.SaveGame(gameModel, fileName);
        }

        public void GameSave()
        {
            List<int> cleard_rooms = new List<int>();
            var rooms = this.farFromFreedomRepository.GameModelMap[this.gameModel.Level - 1].GetEnumerator();
            do
            {
                if (rooms.Current.Value != null && (rooms.Current.Value.Enemies == null || rooms.Current.Value.Enemies.Count == 0))
                {
                    cleard_rooms.Add(rooms.Current.Value.RoomID);
                }
            } while (rooms.MoveNext());

            this.farFromFreedomRepository.SaveGameToXml(this.gameModel.Character, this.gameModel.Level, this.gameModel.RoomID, cleard_rooms);
        }

        public bool GameEnd()
        {
            IMainCharacter character = gameModel.Character;
            bool gameEnded = false;
            if (character.CurrentHealth <= 0)
            {
                gameEnded = true;
            }
            return gameEnded;
        }

        public void ItemPicked()
        {
            IItem itemPicked = null;
            foreach (IItem item in gameModel.Items)
            {
                bool isCollision = item.IsCollision((MainCharacter)gameModel.Character);
                if (isCollision && item.Description != "RoomItem")
                {
                    ItemPickerHelper(item);
                    itemPicked = item;
                }
            }
            if (itemPicked != null)
            {
                gameModel.Items.Remove(itemPicked);
            }
        }

        public void PLayerMove(Key key)
        {

            foreach (var item in gameModel.Items)
            {
                if (item.Description == "RoomItem" && ItemInspect((RoomDecorationItem)item, key))
                {
                    return;
                }
            }

            if (MainCharacterMoveChecker(key, ((MainCharacter)gameModel.Character).Area.Rect))
            {
                return;
            }
            if (key == Key.Up)
            {
                ((MainCharacter)gameModel.Character).MoveUp();
                DirectionChangerHelper(Direction.Up);
            }
            else if (key == Key.Left)
            {
                ((MainCharacter)gameModel.Character).MoveLeft();
                DirectionChangerHelper(Direction.Left);
            }
            else if (key == Key.Right)
            {
                ((MainCharacter)gameModel.Character).MoveRight();
                DirectionChangerHelper(Direction.Right);
            }
            else if (key == Key.Down)
            {
                ((MainCharacter)gameModel.Character).MoveDown();
                DirectionChangerHelper(Direction.Down);
            }
            gameModel.Character.CharacterMoved = true;
        }

        public int PlayerShoot(Key key, int counter)
        {
            double x, y, w, h;
            x = ((MainCharacter)gameModel.Character).Area.Rect.X;
            y = ((MainCharacter)gameModel.Character).Area.Rect.Y;
            w = ((MainCharacter)gameModel.Character).Area.Rect.Width;
            h = ((MainCharacter)gameModel.Character).Area.Rect.Height;
            x += w / 2;
            y += h / 4;
            if (Key.W == key)
            {
                Bullet bullet = new Bullet(new Rect(x, y, 20, 20), Direction.Up);
                gameModel.Character.DirectionHelper.DirectionChanger(Direction.Up);
                gameModel.Bullets.Add(bullet);
                return 0;
            }
            else if (Key.S == key)
            {
                Bullet bullet = new Bullet(new Rect(x, y, 20, 20), Direction.Down);
                gameModel.Character.DirectionHelper.DirectionChanger(Direction.Down);
                gameModel.Bullets.Add(bullet);
                return 0;
            }
            else if (Key.A == key)
            {
                Bullet bullet = new Bullet(new Rect(x, y, 20, 20), Direction.Left);
                gameModel.Character.DirectionHelper.DirectionChanger(Direction.Left);
                gameModel.Bullets.Add(bullet);
                return 0;
            }
            else if (Key.D == key)
            {
                Bullet bullet = new Bullet(new Rect(x, y, 20, 20), Direction.Right);
                gameModel.Character.DirectionHelper.DirectionChanger(Direction.Right);
                gameModel.Bullets.Add(bullet);
                return 0;
            }
            return counter;
        }

        public void BulletMove()
        {
            List<Bullet> bullets = gameModel.Bullets;

            foreach (var bullet in bullets)
            {
                if (bullet.Direction == Direction.Up)
                {
                    bullet.MoveUp();
                }
                else if (bullet.Direction == Direction.Down)
                {
                    bullet.MoveDown();
                }
                else if (bullet.Direction == Direction.Left)
                {
                    bullet.MoveLeft();
                }
                else if (bullet.Direction == Direction.Right)
                {
                    bullet.MoveRight();
                }
            }
        }

        public void HighscoreUp(double highscore)
        {
            this.gameModel.Character.HighscoreUp(highscore);
        }

        public IGameModel GameLoad(string fileName)
        {
            return farFromFreedomRepository.LoadGame(fileName);
        }

        public void levelUp()
        {
            this.currentLevel++;
        }

        public void RoomUp()
        {
            ++this.currentRoom;
            this.RoomLooder();
        }

        public void RoomDown()
        {
            --this.currentRoom;
            this.RoomLooder();
        }

        private void DirectionChangerHelper(Direction direction)
        {
            if (direction != gameModel.Character.DirectionHelper.Direction)
            {
                gameModel.Character.DirectionHelper.DirectionChanger(direction);
            }
        }

        private void ItemPickerHelper(IItem item)
        {
            if (item.GetType() == typeof(Hearth))
            {
                Hearth hearth = (Hearth)item;
                gameModel.Character.CurrentHealthUp(hearth.Health);
            }
            else if (item.GetType() == typeof(Bomb))
            {
                Bomb bomb = (Bomb)item;
            }
            else if (item.GetType() == typeof(Bootle))
            {
                Bootle bootle = (Bootle)item;
                gameModel.Character.SpeedUp(bootle.speed);
            }
            else if (item.GetType() == typeof(Coin))
            {
                Coin coin = (Coin)item;
                gameModel.Character.HighscoreUp(coin.Value);
            }
            else if (item.GetType() == typeof(Money))
            {
                Money money = (Money)item;
                gameModel.Character.HighscoreUp(money.Value);
            }
            else if (item.GetType() == typeof(Shield))
            {
                Shield shield = (Shield)item;
                gameModel.Character.HealthUp(shield.Armor);
            }
            else if (item.GetType() == typeof(Star))
            {
                Star star = (Star)item;
                gameModel.Character.PowerUp(star.Power);
            }
        }

        private bool MainCharacterMoveChecker(Key key, Rect area)
        {
            if (area == null)
            {
                return true;
            }
            else if (area.Left <= 112 && Key.Left == key)
            {
                return true;
            }
            else if (area.Right >= 1180 && Key.Right == key)
            {
                return true;
            }
            else if (area.Top <= 112 && Key.Up == key)
            {
                return true;
            }
            else if (area.Bottom >= 600 && Key.Down == key)
            {
                return true;
            }
            return false;
        }

        private bool EnemyWallInspect(Enemy enemy)
        {
            if (enemy.Area.Rect == null)
            {
                return true;
            }
            else if (enemy.Area.Rect.Left <= 112)
            {
                enemy.MoveRight();
                return true;
            }
            else if (enemy.Area.Rect.Right >= 1180)
            {
                enemy.MoveLeft();
                return true;
            }
            else if (enemy.Area.Rect.Top <= 112)
            {
                enemy.MoveDown();
                return true;
            }
            else if (enemy.Area.Rect.Bottom >= 600)
            {
                enemy.MoveUp();
                return true;
            }
            return false;
        }

        private bool EnemyItemInspect(IItem item, Enemy enemy, Direction direction)
        {
            if (item.IsCollision(enemy))
            {
                if (item.Area.Rect.Left <= enemy.Area.Rect.Left && Direction.Left == direction)
                {
                    enemy.MoveUpRight();
                    return true;
                }
                else if (item.Area.Rect.Right >= enemy.Area.Rect.Right && Direction.Right == direction)
                {
                    enemy.MoveUpLeft();
                    return true;
                }
                else if (item.Area.Rect.Top <= enemy.Area.Rect.Top && Direction.Up == direction)
                {
                    enemy.MoveDown();
                    return true;
                }
                else if (item.Area.Rect.Bottom >= enemy.Area.Rect.Bottom && Direction.Down == direction)
                {
                    enemy.MoveUp();
                    return true;
                }
                else if (item.Area.Rect.BottomRight.X >= enemy.Area.Rect.BottomRight.X && item.Area.Rect.BottomRight.Y >= enemy.Area.Rect.BottomRight.Y && Direction.DownRight == direction)
                {
                    enemy.MoveDown();
                    return true;
                }
                else if (item.Area.Rect.BottomLeft.X <= enemy.Area.Rect.BottomLeft.X && item.Area.Rect.BottomLeft.Y >= enemy.Area.Rect.BottomLeft.Y && Direction.DownLeft == direction)
                {
                    enemy.MoveDown();
                    return true;
                }
                else if (item.Area.Rect.TopRight.X >= enemy.Area.Rect.TopRight.X && item.Area.Rect.TopRight.Y <= enemy.Area.Rect.TopRight.Y && Direction.UpRight == direction)
                {
                    enemy.MoveUp();
                    return true;
                }
                else if (item.Area.Rect.TopLeft.X <= enemy.Area.Rect.TopLeft.X && item.Area.Rect.TopLeft.Y <= enemy.Area.Rect.TopLeft.Y && Direction.UpLeft == direction)
                {
                    enemy.MoveUp();
                    return true;
                }
            }
            return false;
        }

        private void EnemyMover(Enemy enemy, Direction direction)
        {
            if (direction == Direction.Up)
            {
                enemy.MoveUp();
            }
            else if (direction == Direction.UpLeft)
            {
                enemy.MoveUpLeft();
            }
            else if (direction == Direction.UpRight)
            {
                enemy.MoveUpRight();
            }
            else if (direction == Direction.Down)
            {
                enemy.MoveDown();
            }
            else if (direction == Direction.DownLeft)
            {
                enemy.MoveDownLeft();
            }
            else if (direction == Direction.DownRight)
            {
                enemy.MoveDownRight();
            }
            else if (direction == Direction.Right)
            {
                enemy.MoveRight();
            }
            else if (direction == Direction.Left)
            {
                enemy.MoveLeft();
            }
        }

        private bool ItemInspect(IItem item, Key key)
        {
            if (item.IsCollision((MainCharacter)gameModel.Character))
            {
                if (item.Area.Rect.Left <= gameModel.Character.Area.Rect.Left && Key.Left == key)
                {
                    return true;
                }
                else if (item.Area.Rect.Right >= gameModel.Character.Area.Rect.Right && Key.Right == key)
                {
                    return true;
                }
                else if (item.Area.Rect.Top <= gameModel.Character.Area.Rect.Top && Key.Up == key)
                {
                    return true;
                }
                else if (item.Area.Rect.Bottom >= gameModel.Character.Area.Rect.Bottom && Key.Down == key)
                {
                    return true;
                }
            }
            return false;
        }

        private void ItemAdder(IGameModel gameModel, Rect react)
        {
            Random r = new Random();
            int randomNumber = r.Next(0, 18);

            switch (randomNumber)
            {
                case 0:
                    gameModel.Items.Add(new Bootle(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 1:
                    gameModel.Items.Add(new Coin(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 2:
                    gameModel.Items.Add(new Hearth(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 3:
                    gameModel.Items.Add(new Hearth(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 4:
                    gameModel.Items.Add(new Hearth(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 5:
                    gameModel.Items.Add(new Hearth(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 6:
                    gameModel.Items.Add(new Money(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 7:
                    gameModel.Items.Add(new Shield(new Rect(react.Location, new Size(50, 50))));
                    return;
                case 8:
                    gameModel.Items.Add(new Star(new Rect(react.Location, new Size(50, 50))));
                    return;
            }
        }

        private void RoomLooder() => gameModel = farFromFreedomRepository.GameModelMap[currentLevel][currentRoom];

        public int DoorIntersect()
        {
            foreach (Door door in this.gameModel.Doors)
            {
                if (door.IsCollision((GameItem)this.gameModel.Character))
                {
                    return door.RoomId;
                }
            }
            return -1;
        }

        public void DisposeOutOFBoundsTears()
        {
            List<Bullet> removables = new List<Bullet>();
            foreach (Bullet tear in this.gameModel.Bullets)
            {

                if (tear.Area.Rect.Left <= 105)
                {
                    removables.Add(tear);
                }
                else if (tear.Area.Rect.Right >= 1190)
                {
                    removables.Add(tear);
                }
                else if (tear.Area.Rect.Top <= 105)
                {
                    removables.Add(tear);
                }
                else if (tear.Area.Rect.Bottom >= 610)
                {
                    removables.Add(tear);
                }
            }
            removables.ForEach(x => this.gameModel.Bullets.Remove(x));
        }

        public void GenerateDoors()
        {
            double w60percent = 178.0 * 0.6;
            double h60percent = 195 * 0.6;
            double a60percent = 175 * 0.6;
            double vystart = (1290.0 - w60percent) / 2.0;
            double hxstart = (730.0 - a60percent) / 2.0;
            int level = this.gameModel.Level;
            //178,195 fl
            //175,175 jb
            if (this.gameModel.Enemies.Count == 0 && this.gameModel.Doors?.Count == 0)
            {
                if (this.gameModel.UpperNeighbour != -1)
                {
                    this.gameModel.Doors.Add(new Door(level, 1, this.gameModel.UpperNeighbour, new Rect(vystart, 15, w60percent, h60percent)));
                }
                if (this.gameModel.RightNeighbour != -1)
                {
                    this.gameModel.Doors.Add(new Door(level, 2, this.gameModel.RightNeighbour, new Rect(1275 - a60percent, hxstart, a60percent, a60percent)));
                }
                if (this.gameModel.LowerNeighbour != -1)
                {
                    this.gameModel.Doors.Add(new Door(level, 3, this.gameModel.LowerNeighbour, new Rect(vystart, 715 - h60percent, w60percent, h60percent)));
                }
                if (this.gameModel.LeftNeighbour != -1)
                {
                    this.gameModel.Doors.Add(new Door(level, 4, this.gameModel.LeftNeighbour, new Rect(15, hxstart, a60percent, a60percent)));
                }
                if (!this.farFromFreedomRepository.GameModelMap[this.gameModel.Level - 1].Where(x => x.Value.RoomID > this.gameModel.RoomID).Any())
                {
                    this.gameModel.Doors.Add(new Door(level, 5, 999, new Rect(860, 220, 80, 80)));
                }
            }
        }

        public IGameModel ChangeRoom(int roomid)
        {
            int currentLevel = this.gameModel.Level;
            int currentRoomID = this.gameModel.RoomID;

            this.farFromFreedomRepository.GameModelMap[currentLevel - 1][currentRoomID] = this.gameModel;
            IMainCharacter mc = this.gameModel.Character;

            if (roomid == 999)
            {
                if (this.gameModel.Level == 3)
                {
                    this.Win();
                    return this.gameModel;
                }
                mc.Area.Rect = new Rect(604, 312, mc.Area.Rect.Width, mc.Area.Rect.Height);
                mc.Area = new RectangleGeometry(new Rect(604, 312, mc.Area.Rect.Width, mc.Area.Rect.Height));
                this.gameModel = farFromFreedomRepository.GameModelMap[this.gameModel.Level].First().Value;
                this.gameModel.Character = mc;
                return this.gameModel;
            }

            if (roomid == this.gameModel.UpperNeighbour)
            {
                RectangleGeometry newArea = new RectangleGeometry(new Rect(mc.Area.Rect.X, 480, mc.Area.Rect.Width, mc.Area.Rect.Height));
                mc.RepositionByEnteringAnotherRoom(newArea.Rect);
                mc.Area = newArea;
            }
            else if (roomid == this.gameModel.LeftNeighbour)
            {
                RectangleGeometry newArea = new RectangleGeometry(new Rect(1080, mc.Area.Rect.Y, mc.Area.Rect.Width, mc.Area.Rect.Height));
                mc.RepositionByEnteringAnotherRoom(newArea.Rect);
                mc.Area = newArea;
            }
            else if (roomid == this.gameModel.LowerNeighbour)
            {
                RectangleGeometry newArea = new RectangleGeometry(new Rect(mc.Area.Rect.X, 140, mc.Area.Rect.Width, mc.Area.Rect.Height));
                mc.RepositionByEnteringAnotherRoom(newArea.Rect);
                mc.Area = newArea;
            }
            else if (roomid == this.gameModel.RightNeighbour)
            {
                RectangleGeometry newArea = new RectangleGeometry(new Rect(136, mc.Area.Rect.Y, mc.Area.Rect.Width, mc.Area.Rect.Height));
                mc.RepositionByEnteringAnotherRoom(newArea.Rect);
                mc.Area = newArea;
            }


            this.gameModel = farFromFreedomRepository.GameModelMap[this.gameModel.Level - 1][roomid];
            this.gameModel.Character = mc;
            return this.gameModel;
        }

        public void Win()
        {
            this.won = true;
        }
        public void Pause(bool pause)
        {
            if (pause)
            {
                this.gameModel.PauseModel = new PauseModel()
                {
                    IsDead = false,
                };
            }
            else
            {
                this.gameModel.PauseModel = null;
            }
        }

        public void SaveHighscore(string pname)
        {
            this.farFromFreedomRepository.SaveHighScore(pname, (int)this.gameModel.Character.Highscore);
        }
    }
}
