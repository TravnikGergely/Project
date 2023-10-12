using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class MushroomEnemy : Enemy
    {
        [JsonConstructor]
        public MushroomEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public MushroomEnemy()
        {
        }
        public MushroomEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public MushroomEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public override int level => 1;
        public override double Highscore => 10;

        private readonly string name = "mushroomEnemy";
        private readonly string description = "mushroomEnemy";
        private readonly double health = 1;
        private readonly double currentHealth = 1;
        private readonly double power = 1;
    }
}
