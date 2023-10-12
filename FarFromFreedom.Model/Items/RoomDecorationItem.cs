using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class RoomDecorationItem : IItem
    {
        private RectangleGeometry area;
        private string name;
        private string description = "RoomItem";

        public RoomDecorationItem()
        {

        }

        public RoomDecorationItem(string name, Rect area)
        {
            this.name = name;
            this.area = new RectangleGeometry(area);
        }

        public RectangleGeometry Area => area;

        public string Name => name;

        public string Description => description;

        public void setArea(Rect area)
        {
            this.area = new RectangleGeometry(area);
        }
    }
}
