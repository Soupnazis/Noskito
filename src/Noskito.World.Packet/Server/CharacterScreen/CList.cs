using Noskito.Enum.Character;

namespace Noskito.World.Packet.Server.CharacterScreen
{
    public class CList : SPacket
    {
        public byte Slot { get; init; }
        public string Name { get; init; }
        public Gender Gender { get; init; }
        public HairStyle HairStyle { get; init; }
        public HairColor HairColor { get; init; }
        public Job Job { get; init; }
        public byte Level { get; init; }
        public byte HeroLevel { get; init; }
        public byte JobLevel { get; init; }
        public bool Rename { get; init; }
    }

    public class CListCreator : SPacketCreator<CList>
    {
        protected override string CreatePacket(CList source)
        {
            return $"clist {source.Slot} {source.Name} 0 {(byte) source.Gender} " +
                   $"{(byte) source.HairStyle} {(byte) source.HairColor} 0 {(byte)source.Job} {source.Level} {source.HeroLevel} -1.-1 {source.JobLevel}  0 0 0 0";
        }
    }
}