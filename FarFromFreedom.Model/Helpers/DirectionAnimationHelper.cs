using FarFromFreedom.Model.Items;

namespace FarFromFreedom.Model
{
    public class DirectionAnimationHelper
    {
        public Direction Direction => direction;
        private Direction direction = Direction.Up;
        public bool DirectionChanged => directionChanged;
        private bool directionChanged = false;

        public void DefaultDirectionChange()
        {
            directionChanged = false;
        }
        public void DirectionChanger(Direction direction)
        {
            this.direction = direction;
            directionChanged = true;
        }
    }
}
