using System;
using System.Threading.Tasks;
using Noskito.World.Packet.Client;

namespace Noskito.World.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(WorldSession session, CPacket packet);
    }

    public abstract class PacketProcessor<T> : IPacketProcessor where T : CPacket
    {
        public Type PacketType { get; } = typeof(T);

        public Task ProcessPacket(WorldSession session, CPacket packet)
        {
            return Process(session, (T) packet);
        }

        protected abstract Task Process(WorldSession session, T packet);
    }
}