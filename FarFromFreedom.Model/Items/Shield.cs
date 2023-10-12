using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Shield : IItem
    {
        public Shield(Rect area)
        {
            this.area = area;
        }

        public string Name => "Shield";

        public string Description => "Shield";

        public int Armor => 1;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
