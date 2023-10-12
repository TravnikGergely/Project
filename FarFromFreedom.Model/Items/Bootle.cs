using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Bootle : IItem
    {
        public Bootle(Rect area)
        {
            this.area = area;
        }

        public string Name => "Bottle";

        public string Description => "Add Power";

        public double speed => 0.2;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
