using Noskito.Enum;
using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Client.CharacterScreen
{
    public class CharNew : CPacket
    {
        public string Name { get; init; }
        public byte Slot { get; init; }
        public HairColor HairColor { get; init; }
        public HairStyle HairStyle { get; init; }
        public Gender Gender { get; init; }
    }

    public class CharNewCreator : CPacketCreator<CharNew>
    {
        public override string Header { get; } = "Char_NEW";

        protected override CharNew CreatePacket(string[] parameters)
        {
            return new()
            {
                Name = parameters[0],
                Slot = parameters[1].ToByte(),
                Gender = parameters[2].ToEnum<Gender>(),
                HairColor = parameters[3].ToEnum<HairColor>(),
                HairStyle = parameters[4].ToEnum<HairStyle>()
            };
        }
    }
}