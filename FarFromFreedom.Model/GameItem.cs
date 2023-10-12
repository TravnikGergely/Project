using FarFromFreedom.Model.Helpers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model
{
    public abstract class GameItem : IMoveModel
    {
        public GameItem()
        {

        }

        [TypeConverter(typeof(JsonRectConverter))]
        public RectangleGeometry Area { get; set; }

        public Vector Speed => speed;

        private Rect area;

        protected Rect areaRect { set => this.area = value; }

        private Vector speed = new Vector(4, 4);

        protected Vector SpeedSetter { set => this.speed = value; }

        public GameItem(Rect area, Vector speed)
        {
            this.area = area;
            this.speed = speed;
            Area = new RectangleGeometry(area);
        }

        public GameItem(Rect area)
        {
            this.area = area;
            Area = new RectangleGeometry(area);
        }

        public GameItem(string area)
        {
        }

        public bool IsCollision(GameItem other)
        {
            return Geometry.Combine(this.Area, other.Area,
                GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        public void MoveDown()
        {
            area.Y += 1 * speed.Y;
            Area.Rect = area;
        }

        public void MoveUp()
        {
            area.Y -= 1 * speed.Y;
            Area.Rect = area;
        }

        public void MoveLeft()
        {
            area.X -= 1 * speed.X;
            Area.Rect = area;
        }

        public void MoveRight()
        {
            area.X += 1 * speed.X;
            Area.Rect = area;
        }

        public void MoveUpRight()
        {
            area.X += 1 * speed.X;
            area.Y += 1 * speed.Y;
            Area.Rect = area;
        }

        public void MoveDownRight()
        {
            area.X += 1 * speed.X;
            area.Y -= 1 * speed.Y;
            Area.Rect = area;
        }

        public void MoveUpLeft()
        {
            area.X -= 1 * speed.X;
            area.Y += 1 * speed.Y;
            Area.Rect = area;
        }

        public void MoveDownLeft()
        {
            area.X -= 1 * speed.X;
            area.Y -= 1 * speed.Y;
            Area.Rect = area;
        }

        public void SpeedUp(double speed)
        {
            this.speed.X += speed;
            this.speed.Y += speed;
        }
        public void SpeedDown(double speed)
        {
            this.speed.X -= speed;
            this.speed.Y -= speed;
        }
    }
}
