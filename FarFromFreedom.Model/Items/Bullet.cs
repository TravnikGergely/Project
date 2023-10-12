using FarFromFreedom.Model.Items;
using System.Windows;

namespace FarFromFreedom.Model
{
    public class Bullet : GameItem
    {
        public Bullet(Rect area, Direction direction) : base(area)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.direction = direction;
            this.SpeedSetter = new Vector(8, 8);
        }

        private Direction direction;

        public Direction Direction => direction;

    }
}
