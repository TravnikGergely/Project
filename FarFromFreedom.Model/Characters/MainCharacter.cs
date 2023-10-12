using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters
{
    public class MainCharacter : GameItem, IMainCharacter
    {
        [JsonConstructor]
        public MainCharacter(string name, string description, double health, double currentHealth, double power, int coin, Rect area) : base(area)
        {
            this.name = name;
            this.description = description;
            this.health = health;
            this.currentHealth = currentHealth;
            this.power = power;
            this.coin = coin;
            this.SpeedSetter = new Vector(6, 6);
        }

        public MainCharacter()
        {

        }

        public MainCharacter(string name, string description, int health, int currentHealth, Vector speed, int coin, int highscore, int power, Rect area) : base(area)
        {
            this.name = name;
            this.description = description;
            this.health = health;
            this.currentHealth = currentHealth;
            this.SpeedSetter = speed;
            this.coin = coin;
            this.highscore = highscore;
            this.power = power;
            this.RepositionByEnteringAnotherRoom(area);
        }

        private string name;
        private string description;
        private double health;
        private double currentHealth;
        private double power;
        private int coin;
        private double highscore;
        private DirectionAnimationHelper directionHelper = new DirectionAnimationHelper();

        public string Name => name;

        public string Description => description;

        public double Health => health;

        public double Power => power;

        public double CurrentHealth => currentHealth;

        public int Coin => coin;

        public double Highscore => highscore;

        public DirectionAnimationHelper DirectionHelper => directionHelper;

        public bool CharacterMoved { get; set; } = false;


        public void PowerUp(double power)
        {
            this.power += power;
        }

        public void PowerDown(double power)
        {
            this.power -= power;
        }

        public void HealthUp(double health)
        {
            this.health += health;
        }

        public void HealthDown(double health)
        {
            this.health -= health;
        }

        public void CurrentHealthUp(double currentHealth)
        {
            if (this.currentHealth == health)
            {
                return;
            }
            this.currentHealth += currentHealth;
        }

        public void CurrentHealthDown(double currentHealth)
        {
            this.currentHealth -= currentHealth;
        }

        public void CoinUp(int coin)
        {
            this.coin += coin;
        }

        public void CoinDown(int coin)
        {
            this.coin -= coin;
        }

        public void HighscoreUp(double highscore)
        {
            this.highscore += highscore;
        }

        public void RepositionByEnteringAnotherRoom(Rect rect)
        {
            this.areaRect = rect;
        }
    }
}
