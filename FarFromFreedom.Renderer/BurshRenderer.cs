using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Renderer
{
    internal static class BurshRenderer
    {
        //Items
        private static Brush bagBrush = GetBrushes(Path.Combine("Images", "items", "bag.png"));
        private static Brush bombBrush = GetBrushes(Path.Combine("Images", "items", "bomb.png"));
        private static Brush bottleBrush = GetBrushes(Path.Combine("Images", "items", "bottle.png"));
        private static Brush coinBrush = GetBrushes(Path.Combine("Images", "items", "coin.png"));
        private static Brush heartBrush = GetBrushes(Path.Combine("Images", "items", "heart.png"));
        private static Brush emptyheartBrush = GetBrushes(Path.Combine("Images", "items", "emptyheart.png"));
        private static Brush moneyBrush = GetBrushes(Path.Combine("Images", "items", "money.png"));
        private static Brush shieldBrush = GetBrushes(Path.Combine("Images", "items", "shield.png"));
        private static Brush starBrush = GetBrushes(Path.Combine("Images", "items", "star.png"));
        private static Brush tearsBrush = GetBrushes(Path.Combine("Images", "items", "bullet.png"));
        private static Brush timeBrush = GetBrushes(Path.Combine("Images", "items", "time.png"));
        private static Brush welcomePageBrush = GetBrushes(Path.Combine("Images", "background", "Start.jpg"));

        private static Brush bigSkull = GetBrushes(Path.Combine("Images", "background", "items", "bigSkull.png"));
        private static Brush bigStone = GetBrushes(Path.Combine("Images", "background", "items", "bigStone.png"));
        private static Brush board = GetBrushes(Path.Combine("Images", "background", "items", "board.png"));
        private static Brush bush1 = GetBrushes(Path.Combine("Images", "background", "items", "bush1.png"));
        private static Brush bush2 = GetBrushes(Path.Combine("Images", "background", "items", "bush2.png"));
        private static Brush bush3 = GetBrushes(Path.Combine("Images", "background", "items", "bush3.png"));
        private static Brush bush4 = GetBrushes(Path.Combine("Images", "background", "items", "bush4.png"));
        private static Brush bush5 = GetBrushes(Path.Combine("Images", "background", "items", "bush5.png"));
        private static Brush cactus = GetBrushes(Path.Combine("Images", "background", "items", "cactus.png"));
        private static Brush cactus2 = GetBrushes(Path.Combine("Images", "background", "items", "cactus2.png"));
        private static Brush fireplace = GetBrushes(Path.Combine("Images", "background", "items", "fireplace.png"));
        private static Brush fountain = GetBrushes(Path.Combine("Images", "background", "items", "fountain.png"));
        private static Brush palmtree = GetBrushes(Path.Combine("Images", "background", "items", "palmtree.png"));
        private static Brush skull = GetBrushes(Path.Combine("Images", "background", "items", "skull.png"));
        private static Brush stone = GetBrushes(Path.Combine("Images", "background", "items", "stone.png"));
        private static Brush stone2 = GetBrushes(Path.Combine("Images", "background", "items", "stone2.png"));
        private static Brush stone3 = GetBrushes(Path.Combine("Images", "background", "items", "stone3.png"));
        private static Brush stone4 = GetBrushes(Path.Combine("Images", "background", "items", "stone4.png"));
        private static Brush stone5 = GetBrushes(Path.Combine("Images", "background", "items", "stone5.png"));
        private static Brush tree1 = GetBrushes(Path.Combine("Images", "background", "items", "tree1.png"));
        private static Brush tree2 = GetBrushes(Path.Combine("Images", "background", "items", "tree2.png"));
        private static Brush tree3 = GetBrushes(Path.Combine("Images", "background", "items", "tree3.png"));
        private static Brush tree4 = GetBrushes(Path.Combine("Images", "background", "items", "tree4.png"));
        private static Brush well = GetBrushes(Path.Combine("Images", "background", "items", "well.png"));

        private static ImageBrush GetBrushes(string file) => new ImageBrush(new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)));
        internal static Dictionary<string, Brush> Init()
        {
            Dictionary<string, Brush> initGameDrawings = new Dictionary<string, Brush>();

            initGameDrawings.Add("TearsBrush", tearsBrush);

            //Items
            initGameDrawings.Add("Bag", bagBrush);
            initGameDrawings.Add("Bomb", bombBrush);
            initGameDrawings.Add("Bottle", bottleBrush);
            initGameDrawings.Add("EmptyHeart", emptyheartBrush);
            initGameDrawings.Add("Coin", coinBrush);
            initGameDrawings.Add("Heart", heartBrush);
            initGameDrawings.Add("Money", moneyBrush);
            initGameDrawings.Add("Star", starBrush);
            initGameDrawings.Add("Shield", shieldBrush);

            initGameDrawings.Add("bigSkull", bigSkull);
            initGameDrawings.Add("bigStone", bigStone);
            initGameDrawings.Add("board", board);
            initGameDrawings.Add("bush1", bush1);
            initGameDrawings.Add("bush2", bush2);
            initGameDrawings.Add("bush3", bush3);
            initGameDrawings.Add("bush4", bush4);
            initGameDrawings.Add("bush5", bush5);
            initGameDrawings.Add("cactus", cactus);
            initGameDrawings.Add("cactus2", cactus2);
            initGameDrawings.Add("fireplace", fireplace);
            initGameDrawings.Add("fountain", fountain);
            initGameDrawings.Add("palmtree", palmtree);
            initGameDrawings.Add("skull", skull);
            initGameDrawings.Add("stone", stone);
            initGameDrawings.Add("stone2", stone2);
            initGameDrawings.Add("stone3", stone3);
            initGameDrawings.Add("stone4", stone4);
            initGameDrawings.Add("stone5", stone5);
            initGameDrawings.Add("tree1", tree1);
            initGameDrawings.Add("tree2", tree2);
            initGameDrawings.Add("tree3", tree3);
            initGameDrawings.Add("tree4", tree4);
            initGameDrawings.Add("well", well);

            return initGameDrawings;
        }
    }
}
