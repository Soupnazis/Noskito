namespace Noskito.World.Game.Entities
{
    public abstract class LivingEntity : Entity
    {
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }

        public bool IsAlive => Hp > 0;
    }
}