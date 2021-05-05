using System.Collections.Generic;
using Noskito.Enum;
using Noskito.World.Packet.Extension;

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
        public int Level { get; init; }
        public int HeroLevel { get; init; }
        public IEnumerable<short?> Equipments { get; init; }
        public int JobLevel { get; init; }
        public byte QuestCompletion { get; init; }
        public byte QuestPart { get; init; }
        public IEnumerable<short?> Pets { get; init; }
        public int Design { get; init; }
        public bool Rename { get; init; }
    }

    public class CListCreator : SPacketCreator<CList>
    {
        protected override string CreatePacket(CList source)
        {
            return "clist " +
                   $"{source.Slot} " +
                   $"{source.Name} " +
                   "0 " +
                   $"{source.Gender.Format()} " +
                   $"{source.HairStyle.Format()} " +
                   $"{source.HairColor.Format()} " +
                   "0 " +
                   $"{source.Job.Format()} " +
                   $"{source.Level} " +
                   $"{source.HeroLevel} " +
                   $"{source.Equipments.Format()} " +
                   $"{source.JobLevel}  " +
                   $"{source.QuestCompletion} " +
                   $"{source.QuestPart} " +
                   $"{source.Pets.Format()} " +
                   $"{source.Design} " +
                   $"{source.Rename.Format()}";
        }
    }
}