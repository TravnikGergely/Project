using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class ZombieWalkCrippleEnemy : Enemy
    {
        [JsonConstructor]
        public ZombieWalkCrippleEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public ZombieWalkCrippleEnemy()
        {

        }
        public ZombieWalkCrippleEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public ZombieWalkCrippleEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        private readonly string name = "zombieWalkCrippleEnemy";
        private readonly string description = "ZombieWalkCrippleEnemy";
        private readonly double health = 4;
        private readonly double currentHealth = 4;
        private readonly double power = 1;

        public override int level => 3;
        public override double Highscore => 30;
    }
}
