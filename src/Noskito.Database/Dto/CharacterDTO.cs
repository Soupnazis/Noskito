using Noskito.Enum;

namespace Noskito.Database.Dto
{
    public class CharacterDTO
    {
        public long Id { get; init; }
        public long AccountId { get; init; }
        public byte Slot { get; init; }
        public string Name { get; init; }
        
        public int Level { get; init; }
        public int JobLevel { get; init; }
        public int HeroLevel { get; init; }
        
        public long Experience { get; init; }
        public long JobExperience { get; init; }
        public long HeroExperience { get; init; }
        
        public long Reputation { get; init; }
        public int Dignity { get; init; }
        
        public int Hp { get; init; }
        public int Mp { get; init; }
        
        public int X { get; init; }
        public int Y { get; init; }
        public int MapId { get; init; }
        
        public Job Job { get; init; }
        public HairColor HairColor { get; init; }
        public HairStyle HairStyle { get; init; }
        public Gender Gender { get; init; }
    }
}