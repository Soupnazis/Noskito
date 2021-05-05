using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Server.Maps
{
    public class CMap : SPacket
    {
        public int MapId { get; init; }
        public bool IsJoining { get; init; }
    }

    public class CMapCreator : SPacketCreator<CMap>
    {
        protected override string CreatePacket(CMap source)
        {
            return "c_map " +
                   "0 " +
                   $"{source.MapId} " +
                   $"{source.IsJoining.Format()}";
        }
    }
}