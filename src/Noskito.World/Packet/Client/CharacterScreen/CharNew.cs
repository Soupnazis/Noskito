using System;
using Noskito.Enum.Character;

namespace Noskito.World.Packet.Client.CharacterScreen
{
    public class CharNew : CPacket
    {
        public string Name { get; init; }
        public byte Slot { get; init; }
        public HairColor HairColor { get; init; }
        public HairStyle HairStyle { get; init; }
        public Gender Gender { get; init; }
        public bool IsMartialArtist { get; init; }
    }
    
    public class CharNewCreator : CPacketCreator<CharNew>
    {
        protected override CharNew CreatePacket(string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}