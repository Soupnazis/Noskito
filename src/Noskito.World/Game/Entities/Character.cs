using System;
using Noskito.Enum;

namespace Noskito.World.Game.Entities
{
    public class Character : LivingEntity
    {
        public Character(WorldSession session)
        {
            EntityType = EntityType.Player;
            Session = session;
        }

        public Job Job { get; set; }
        public HairStyle HairStyle { get; set; }
        public HairColor HairColor { get; set; }
        public Gender Gender { get; set; }

        public int Level { get; set; }
        public int JobLevel { get; set; }
        public int HeroLevel { get; set; }
        
        public long Experience { get; set; }
        public long JobExperience { get; set; }
        public long HeroExperience { get; set; }
        
        public long Reputation { get; set; }
        public int Dignity { get; set; }

        public WorldSession Session { get; }
    }
}