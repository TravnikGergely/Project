using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Star : IItem
    {
        public Star(Rect area)
        {
            this.area = area;
        }

        public string Name => "Star";
        public string Description => "Super Power";
        public double Power => 1;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
