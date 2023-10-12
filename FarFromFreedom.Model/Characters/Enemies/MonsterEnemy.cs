using Newtonsoft.Json;
using System.Windows;

namespace FarFromFreedom.Model.Characters.Enemies
{
    public class MonsterEnemy : Enemy
    {
        [JsonConstructor]
        public MonsterEnemy(string name, string description, double health, double power, double currentHealth, Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }
        public MonsterEnemy()
        {
        }
        public MonsterEnemy(Rect area, Vector speed) : base(area, speed)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public MonsterEnemy(Rect area) : base(area)
        {
            this.initProperty(name, description, health, currentHealth, power);
        }

        public override int level => 2;
        public override double Highscore => 20;

        private readonly string name = "monsterEnemy";
        private readonly string description = "monsterEnemy";
        private readonly double health = 2;
        private readonly double currentHealth = 2;
        private readonly double power = 1;
    }
}
