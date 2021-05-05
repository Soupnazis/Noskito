using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Client.Player
{
    public class Walk : CPacket
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Checksum { get; init; }
        public int Speed { get; init; }
    }

    public class WalkCreator : CPacketCreator<Walk>
    {
        protected override Walk CreatePacket(string[] parameters)
        {
            return new()
            {
                X = parameters[0].ToInt(),
                Y = parameters[1].ToInt(),
                Checksum = parameters[2].ToInt(),
                Speed = parameters[3].ToInt()
            };
        }
    }
}