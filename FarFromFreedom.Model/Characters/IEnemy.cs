namespace FarFromFreedom.Model.Characters
{
    public interface IEnemy
    {
        double CurrentHealth { get; }
        string Description { get; }
        double Health { get; }
        string Name { get; }
        double Power { get; }
        void CurrentHealthDown(double currentHealth);
        void CurrentHealthUp(double currentHealth);
        void HealthDown(double health);
        void HealthUp(double health);
        void PowerDown(double power);
        void PowerUp(double power);
    }
}