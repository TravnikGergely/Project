using System.Windows;

namespace FarFromFreedom.Model
{
    public interface IMoveModel
    {
        Vector Speed { get; }

        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        public void MoveUpRight();
        public void MoveDownRight();
        public void MoveUpLeft();
        public void MoveDownLeft();
        public void SpeedUp(double speed);
        public void SpeedDown(double speed);
    }
}
