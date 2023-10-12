using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Renderer
{
    internal static class BackgroundRenderer
    {
        private static Brush Level1Start_base = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1_start.jpg"));
        private static Brush Level1_base = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1_base.jpg"));
        private static Brush Level2_base = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2.jpg"));
        private static Brush Level3_base = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3.png"));
        private static Brush Level1door_1 = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1door1_small.png"));
        private static Brush Level1door_2 = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1door2_small.png"));
        private static Brush Level1door_3 = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1door3_small.png"));
        private static Brush Level1door_4 = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1door4_small.png"));
        private static Brush Level1door_5 = GetBrushes(Path.Combine("Images", "levels", "Level1", "Level1door5_small.png"));

        private static Brush Level2door_1 = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2doors1.png"));
        private static Brush Level2door_2 = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2doors2.png"));
        private static Brush Level2door_3 = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2doors3.png"));
        private static Brush Level2door_4 = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2doors4.png"));
        private static Brush Level2door_5 = GetBrushes(Path.Combine("Images", "levels", "Level2", "Level2door5_small.png"));

        private static Brush Level3door_1 = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3doors1.png"));
        private static Brush Level3door_2 = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3doors2.png"));
        private static Brush Level3door_3 = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3doors3.png"));
        private static Brush Level3door_4 = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3doors4.png"));
        private static Brush Level3door_5 = GetBrushes(Path.Combine("Images", "levels", "Level3", "Level3door5_small.png"));

        private static Brush PauseMenu = GetBrushes(Path.Combine("Images", "levels", "PausedMenu.png"));
        private static Brush DeathImage = GetBrushes(Path.Combine("Images", "levels", "DiedPicture.png"));
        private static Brush FinalMenu = GetBrushes(Path.Combine("Images", "levels", "finalMenu.png"));

        private static Brush Level2 = GetBrushes(Path.Combine("Images", "background", "Level2.jpg"));
        private static Brush Level3 = GetBrushes(Path.Combine("Images", "background", "Level3.jpg"));

        private static ImageBrush GetBrushes(string file) => new ImageBrush(new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)));
        internal static Dictionary<string, Brush> Init()
        {
            Dictionary<string, Brush> initGameDrawings = new Dictionary<string, Brush>();

            initGameDrawings.Add("Level1Start_base", Level1Start_base);
            initGameDrawings.Add("Level1_base", Level1_base);
            initGameDrawings.Add("Level1door1", Level1door_1);
            initGameDrawings.Add("Level1door2", Level1door_2);
            initGameDrawings.Add("Level1door3", Level1door_3);
            initGameDrawings.Add("Level1door4", Level1door_4);
            initGameDrawings.Add("Level1door5", Level1door_5);
            initGameDrawings.Add("Level2_base", Level2_base);
            initGameDrawings.Add("Level2door1", Level2door_1);
            initGameDrawings.Add("Level2door2", Level2door_2);
            initGameDrawings.Add("Level2door3", Level2door_3);
            initGameDrawings.Add("Level2door4", Level2door_4);
            initGameDrawings.Add("Level2door5", Level2door_5);
            initGameDrawings.Add("Level3_base", Level3_base);
            initGameDrawings.Add("Level3door1", Level3door_1);
            initGameDrawings.Add("Level3door2", Level3door_2);
            initGameDrawings.Add("Level3door3", Level3door_3);
            initGameDrawings.Add("Level3door4", Level3door_4);
            initGameDrawings.Add("Level3door5", Level3door_5);
            initGameDrawings.Add("PauseMenu", PauseMenu);
            initGameDrawings.Add("DeathImage", DeathImage);
            initGameDrawings.Add("FinalMenu", FinalMenu);


            //initGameDrawings.Add("Level1", Leve1);
            //initGameDrawings.Add("Level1Start", Level1Start);
            //initGameDrawings.Add("Level2", Level2);
            //initGameDrawings.Add("Level3", Level3);

            return initGameDrawings;
        }
    }
}
