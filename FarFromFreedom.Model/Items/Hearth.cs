using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Hearth : IItem
    {
        public Hearth(Rect area)
        {
            this.area = area;
        }

        public string Name => "Heart";
        public string Description => "Add health";
        public double Health => 1;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
