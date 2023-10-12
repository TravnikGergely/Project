using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class FlyingEnemy : Enemy
    {
        [JsonConstructor]
        public FlyingEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public FlyingEnemy()
        {

        }
        public FlyingEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public FlyingEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        private readonly string name = "flyingEnemy";
        private readonly string description = "FlyingEnemy";
        private readonly double health = 2;
        private readonly double currentHealth = 2;
        private readonly double power = 1;

        public override int level => 2;
        public override double Highscore => 20;
    }
}
