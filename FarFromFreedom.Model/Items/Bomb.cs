using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Bomb : IItem
    {
        public Bomb(Rect area)
        {
            this.area = area;
        }

        public string Name => "Bomb";

        public string Description => "Booommm";

        public int Damage => 3;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
