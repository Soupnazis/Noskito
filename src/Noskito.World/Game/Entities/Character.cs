using Noskito.Enum;

namespace Noskito.World.Game.Entities
{
    public class Character : LivingEntity
    {
        public Job Job { get; set; }
        public HairStyle HairStyle { get; set; }
        public HairColor HairColor { get; set; }
        public Gender Gender { get; set; }
        
        public int Level { get; set; }
        public int JobLevel { get; set; }
        public int HeroLevel { get; set; }

        public WorldSession Session { get; }
        
        public Character(WorldSession session)
        {
            EntityType = EntityType.Player;
            Session = session;
        }
    }
}