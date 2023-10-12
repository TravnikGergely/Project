using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Money : IItem
    {
        public Money(Rect area)
        {
            this.area = area;
        }

        public string Name => "Money";

        public string Description => "Lot of coin";

        public int Value => 10;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
