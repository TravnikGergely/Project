using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class CatEnemy : Enemy
    {
        [JsonConstructor]
        public CatEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public CatEnemy()
        {
        }
        public CatEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public CatEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public override int level => 3;
        public override double Highscore => 30;

        private readonly string name = "catEnemy";
        private readonly string description = "catEnemy";
        private readonly double health = 2;
        private readonly double currentHealth = 2;
        private readonly double power = 1;
    }
}
