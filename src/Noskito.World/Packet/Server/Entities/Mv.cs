using Noskito.Enum;
using Noskito.World.Packet.Extension;

namespace Noskito.World.Packet.Server.Entities
{
    public class Mv : SPacket
    {
        public EntityType EntityType { get; init; }
        public long EntityId { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public int Speed { get; init; }
    }

    public class MvCreator : SPacketCreator<Mv>
    {
        protected override string CreatePacket(Mv source)
        {
            return "mv " +
                   $"{source.EntityType.Format()} " +
                   $"{source.EntityId} " +
                   $"{source.X} " +
                   $"{source.Y} " +
                   $"{source.Speed}";
        }
    }
}