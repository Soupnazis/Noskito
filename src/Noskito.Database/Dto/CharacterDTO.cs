using Noskito.Enum.Character;

namespace Noskito.Database.Dto
{
    public class CharacterDTO
    {
        public long Id { get; init; }
        public long AccountId { get; init; }
        public byte Slot { get; init; }
        public string Name { get; init; }
        public byte Level { get; init; }
        public Job Job { get; set; }
        public HairColor HairColor { get; set; }
        public HairStyle HairStyle { get; set; }
        public Gender Gender { get; set; }
    }
}