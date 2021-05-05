using Noskito.Enum;
using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Server.Player
{
    public class CInfo : SPacket
    {
        public string Name { get; init; }
        public int? GroupId { get; init; }
        public int? FamilyId { get; init; }
        public string FamilyName { get; init; }
        public long CharacterId { get; init; }
        public AuthorityType AuthorityType { get; init; }
        public Gender Gender { get; init; }
        public HairStyle HairStyle { get; init; }
        public HairColor HairColor { get; init; }
        public Job Job { get; init; }
        public byte Icon { get; init; }
        public short Compliment { get; init; }
        public short Morph { get; init; }
        public bool Invisible { get; init; }
        public byte FamilyLevel { get; init; }
        public byte MorphUpgrade { get; init; }
        public bool ArenaWinner { get; init; }
    }
    
    public class CInfoCreator : SPacketCreator<CInfo>
    {
        protected override string CreatePacket(CInfo source)
        {
            return $"c_info " +
                   $"{source.Name} " +
                   $"- " +
                   $"{source.GroupId.Format()} " +
                   $"{source.FamilyId.Format()} " +
                   $"{source.FamilyName.Format()} " +
                   $"{source.CharacterId} " +
                   $"{source.AuthorityType.Format()} " +
                   $"{source.Gender.Format()} " +
                   $"{source.HairStyle.Format()} " +
                   $"{source.HairColor.Format()} " +
                   $"{source.Job.Format()} " +
                   $"{source.Icon} " +
                   $"{source.Compliment} " +
                   $"{source.Morph} " +
                   $"{source.Invisible.Format()} " +
                   $"{source.FamilyLevel} " +
                   $"{source.MorphUpgrade} " +
                   $"{source.ArenaWinner.Format()}";
        }
    }
}