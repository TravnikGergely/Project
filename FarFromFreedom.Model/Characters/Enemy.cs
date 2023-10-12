using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Model.Characters
{
    public abstract class Enemy : GameItem, IEnemy
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Enemy(Rect area, Vector speed) : base(area, speed)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Enemy()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Enemy(Rect area) : base(area)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        private protected void initProperty(string name, string description, double health, double currentHealth, double power)
        {
            this.name = name;
            this.description = description;
            this.health = health;
            this.currentHealth = currentHealth;
            this.power = power;
        }

        private string name;
        private string description;
        private double health;
        private double currentHealth;
        private double power;
        private int counter = 0;

        public abstract double Highscore { get; }
        private int CounterLength { get => Directory.GetFiles(EnemyPath, "*", SearchOption.AllDirectories).Length - 1; }

        public abstract int level { get; }

        string EnemyPath => Path.Combine("Images", "enemies", $"level{level}", Name);

        protected ImageBrush GetBrushes(string file) => new ImageBrush(new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)));

        public Dictionary<string, Brush> ImageBurshes => ImageBurshesInit();
        public int Counter { get => counter; }
        public string Name => name;

        public string Description => description;

        public double Health => health;

        public double Power => power;

        public double CurrentHealth => currentHealth;

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
            this.currentHealth += currentHealth;
        }

        public void CurrentHealthDown(double currentHealth)
        {
            this.currentHealth -= currentHealth;
        }

        public void counterUp()
        {
            counter += 1;
            if (CounterLength < Counter)
            {
                counter = 0;
            }
        }

        private Dictionary<string, Brush> ImageBurshesInit()
        {
            Dictionary<string, Brush> enemies = new Dictionary<string, Brush>();


            for (int i = 0; i <= CounterLength; i++)
            {
                enemies.Add($"{Name}{i}", GetBrushes(Path.Combine(EnemyPath, $"{Name}{i}.png")));
            }

            return enemies;
        }

    }
}
