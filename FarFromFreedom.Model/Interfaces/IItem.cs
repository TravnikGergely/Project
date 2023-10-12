using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model
{
    public interface IItem
    {
        RectangleGeometry Area { get; }
        string Name { get; }
        string Description { get; }

        public bool IsCollision(GameItem other)
        {
            return Geometry.Combine(this.Area, other.Area,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }
        void setArea(Rect area);
    }
}