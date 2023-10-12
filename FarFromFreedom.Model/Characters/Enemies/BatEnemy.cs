using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class BatEnemy : Enemy
    {

        [JsonConstructor]
        public BatEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public BatEnemy()
        {
        }
        public BatEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public BatEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public override int level => 2;
        public override double Highscore => 20;

        private readonly string name = "batEnemy";
        private readonly string description = "BatEnemy";
        private readonly double health = 2;
        private readonly double currentHealth = 2;
        private readonly double power = 1;
    }
}
