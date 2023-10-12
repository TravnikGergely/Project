using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Renderer
{
    public class GameRenderer : FrameworkElement, IGameRenderer
    {
        private Dictionary<string, Brush> GameBrushes;
        private MainCharacterRender mainCharacterRenderer;
        private Dictionary<string, Brush> backGroundBrushes = BackgroundRenderer.Init();
        private IGameModel model;
        private int counter;
        public GameRenderer(IGameModel model)
        {
            mainCharacterRenderer = new MainCharacterRender(model.Character);
            this.model = model;
            GameBrushes = BurshRenderer.Init();
            counter = 0;
        }

        public DrawingGroup BuildDrawing()
        {
            DrawingGroup drawingGroup = new DrawingGroup();

            this.DrawBackgroung(drawingGroup);

            this.DrawEnemies(drawingGroup);

            this.DrawItems(drawingGroup);

            this.DrawTears(drawingGroup);

            this.DrawMainCharacter(drawingGroup);

            this.DrawInterface(drawingGroup);

            if (this.model.PauseModel != null)
            {
                this.DrawPauseMenu(drawingGroup);
            }

            if (this.model.Won)
            {
                this.DrawWinMenu(drawingGroup);
            }

            return drawingGroup;
        }

        private void DrawPauseMenu(DrawingGroup drawingGroup)
        {
            double f_w = 1290;
            double f_h = 730;
            double w = 1200 * 0.7;
            double h = 1000 * 0.7;
            double x = (f_w - w) / 2;
            double y = (f_h - h) / 2;

            Brush menu = backGroundBrushes["PauseMenu"];
            menu.Opacity = 1;

            if (!this.model.PauseModel.IsDead)
            {
                drawingGroup.Children.Add(GetDrawing(backGroundBrushes["PauseMenu"], new RectangleGeometry(new Rect(x, y, w, h))));
                drawingGroup.Children.Add(this.GetDrawing(Continue, new RectangleGeometry(new Rect(475, 430, 350, 60))));
                drawingGroup.Children.Add(this.GetDrawing(ExitMenu, new RectangleGeometry(new Rect(475, 530, 350, 60))));
            }
            else
            {
                drawingGroup.Children.Add(GetDrawing(backGroundBrushes["DeathImage"], new RectangleGeometry(new Rect(0, 0, 1290, 730))));
            }
        }

        private void DrawWinMenu(DrawingGroup drawingGroup)
        {
            drawingGroup.Children.Add(GetDrawing(backGroundBrushes["FinalMenu"], new RectangleGeometry(new Rect(0, 0, 1290, 730))));
        }

        private void DrawInterface(DrawingGroup drawingGroup)
        {
            Brush heartBrush = GameBrushes.GetValueOrDefault("Heart");
            Brush emptyHeartBrush = GameBrushes.GetValueOrDefault("EmptyHeart");

            double currentHearts = (this.model.Character.CurrentHealth);
            double emptyCurrentHearts = ((this.model.Character.Health) - (this.model.Character.CurrentHealth));

            int hearts = (int)Math.Ceiling(currentHearts);
            int emptyhears = (int)Math.Floor(emptyCurrentHearts);

            double x = 20.0;
            double y = 20.0;
            double w = 69.0 * 0.60;
            double h = 62.0 * 0.60;
            double spacing = 10.0;

            //w69 h62 
            for (int i = 1; i <= hearts; i++)
            {
                drawingGroup.Children.Add(this.GetDrawing(heartBrush, new RectangleGeometry(new Rect(x, y, w, h))));
                x += w + spacing;
            }

            for (int i = 1; i <= emptyhears; i++)
            {
                drawingGroup.Children.Add(this.GetDrawing(emptyHeartBrush, new RectangleGeometry(new Rect(x, y, w, h))));
                x += w + spacing;
            }


            string hs = this.model.Character.Highscore.ToString();
            FormattedText formattedText = new FormattedText(
              hs,
              System.Globalization.CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight,
              new Typeface("Arial"),
              30,
              Brushes.Black,
              1);
            GeometryDrawing highscore = new GeometryDrawing(null, new Pen(Brushes.SandyBrown, 2), formattedText.BuildGeometry(new Point(1192 - 24 - (hs.Length * 13), 20)));

            drawingGroup.Children.Add(highscore);


        }

        private void DrawMainCharacter(DrawingGroup drawingGroup)
        {
            if (model.Character is MainCharacter mainCharacter)
            {
                if (model.Character.CharacterMoved == false)
                {
                    if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Down)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyFront[mainCharacterRenderer.Counter], mainCharacter.Area));
                    }
                    else if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Right)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyRight[mainCharacterRenderer.Counter], mainCharacter.Area));
                    }
                    else if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Left)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyLeft[mainCharacterRenderer.Counter], mainCharacter.Area));
                    }
                    else
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyBack[mainCharacterRenderer.Counter], mainCharacter.Area));
                    }
                }
                else
                {
                    if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Down)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyFront[mainCharacterRenderer.Counter], mainCharacter.Area));
                        mainCharacterRenderer.counterUp();
                    }
                    else if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Right)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyRight[mainCharacterRenderer.Counter], mainCharacter.Area));
                        mainCharacterRenderer.counterUp();
                    }
                    else if (model.Character.DirectionHelper.Direction == Model.Items.Direction.Left)
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyLeft[mainCharacterRenderer.Counter], mainCharacter.Area));
                        mainCharacterRenderer.counterUp();
                    }
                    else
                    {
                        drawingGroup.Children.Add(GetDrawing(mainCharacterRenderer.gobbyBack[mainCharacterRenderer.Counter], mainCharacter.Area));
                        mainCharacterRenderer.counterUp();
                    }
                }

            }

        }

        private void DrawTears(DrawingGroup drawingGroup)
        {
            foreach (var bullet in model.Bullets)
            {
                RectangleGeometry bulletArea = new RectangleGeometry(new Rect(
                    bullet.Area.Rect.X,
                    bullet.Area.Rect.Y,
                    bullet.Area.Rect.Width * (1 + (this.model.Character.Power / 5)),
                    bullet.Area.Rect.Height * (1 + (this.model.Character.Power / 5))
                    ));

                Brush itemBrush = GameBrushes.GetValueOrDefault("TearsBrush");
                if (itemBrush != null)
                {

                    drawingGroup.Children.Add(GetDrawing(itemBrush, bulletArea));

                }
            }
        }

        private void DrawItems(DrawingGroup drawingGroup)
        {
            foreach (var item in model.Items)
            {
                Brush itemBrush = GameBrushes.GetValueOrDefault(item.Name);
                if (itemBrush != null)
                {
                    drawingGroup.Children.Add(GetDrawing(itemBrush, item.Area));
                }
            }
        }

        private void DrawEnemies(DrawingGroup drawingGroup)
        {
            foreach (Enemy enemy in model.Enemies)
            {
                Brush itemBrush = enemy.ImageBurshes.GetValueOrDefault(enemy.Name + enemy.Counter);
                if (itemBrush != null)
                {
                    drawingGroup.Children.Add(GetDrawing(itemBrush, enemy.Area));
                    enemy.counterUp();
                }
            }
        }

        private void DrawBackgroung(DrawingGroup drawingGroup)
        {
            int levee = this.model.Level;

            if (this.model.Level == 1 && this.model.RoomID == 1)
            {
                drawingGroup.Children.Add(GetDrawing(backGroundBrushes[$"Level{this.model.Level}Start_base"], new RectangleGeometry(new Rect(0, 0, 1290, 730))));
            }
            else
            {
                drawingGroup.Children.Add(GetDrawing(backGroundBrushes[$"Level{this.model.Level}_base"], new RectangleGeometry(new Rect(0, 0, 1290, 730))));
            }



            if (this.model.Doors.Count != 0)
            {
                foreach (Door door in this.model.Doors)
                {
                    drawingGroup.Children.Add(GetDrawing(backGroundBrushes[door.Description], door.Area));
                }
            }

        }

        public void GameModelChanged(IModel model) => this.model = (model as IGameModel);


        private Drawing GetDrawing(Brush brush, RectangleGeometry rectangleGeometry)
        {
            GeometryDrawing drawing = new GeometryDrawing(brush, null, rectangleGeometry);
            return drawing;
        }
        private Brush Continue
        {
            get
            {
                Brush brush = GetBrushes(Path.Combine("Images", "MainMenu", "Resume.png"));
                brush.Opacity = this.model.PauseModel.ContinueOpacity;
                return brush;
            }
        }
        private Brush ExitMenu
        {
            get
            {
                Brush brush = GetBrushes(Path.Combine("Images", "MainMenu", "ExitGame.png"));
                brush.Opacity = this.model.PauseModel.SaveOpacity;
                return brush;
            }
        }

        private static ImageBrush GetBrushes(string file) => new ImageBrush(new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)));
    }
}
