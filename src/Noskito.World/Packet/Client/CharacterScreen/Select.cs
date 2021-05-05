using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Client.CharacterScreen
{
    public class Select : CPacket
    {
        public byte Slot { get; init; }
    }

    public class SelectCreator : CPacketCreator<Select>
    {
        protected override Select CreatePacket(string[] parameters)
        {
            return new()
            {
                Slot = parameters[0].ToByte()
            };
        }
    }
}