using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Characters
{
    public interface IMainCharacter
    {
        double CurrentHealth { get; }
        string Description { get; }
        double Health { get; }
        string Name { get; }
        double Power { get; }
        int Coin { get; }
        double Highscore { get; }
        bool CharacterMoved { get; set; }
        RectangleGeometry Area { get; set; }
        Vector Speed { get; }
        DirectionAnimationHelper DirectionHelper { get; }
        void CurrentHealthUp(double currentHealth);
        void CurrentHealthDown(double currentHealth);
        void HealthUp(double health);
        void HealthDown(double health);
        void PowerUp(double power);
        void PowerDown(double power);
        void CoinUp(int coin);
        void CoinDown(int coin);
        void HighscoreUp(double Highscore);
        void SpeedUp(double speed);
        void RepositionByEnteringAnotherRoom(Rect rect);
    }
}