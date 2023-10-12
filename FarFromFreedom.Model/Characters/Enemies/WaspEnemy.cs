using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class WaspEnemy : Enemy
    {
        [JsonConstructor]
        public WaspEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public WaspEnemy()
        {
        }
        public WaspEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public WaspEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public override int level => 1;
        public override double Highscore => 10;

        private readonly string name = "waspEnemy";
        private readonly string description = "waspEnemy";
        private readonly double health = 1;
        private readonly double currentHealth = 1;
        private readonly double power = 1;
    }
}
