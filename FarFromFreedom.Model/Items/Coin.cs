using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Items
{
    public class Coin : IItem
    {
        public Coin(Rect area)
        {
            this.area = area;
        }

        [JsonConstructor]
        public Coin(string name, string descritpon, int value, Rect area)
        {
            this.area = area;
        }
        public Coin()
        {

        }

        public string Name => "Coin";
        public string Description => "Coin";
        public int Value => 3;

        public RectangleGeometry Area => new RectangleGeometry(area);

        private Rect area;

        public void setArea(Rect area)
        {
            this.area = area;
        }
    }
}
