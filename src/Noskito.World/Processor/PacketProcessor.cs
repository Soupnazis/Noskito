using System;
using System.Threading.Tasks;
using Noskito.World.Packet.Client;

namespace Noskito.World.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(WorldSession client, CPacket packet);
    }
    
    public abstract class PacketProcessor<T> : IPacketProcessor where T : CPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public Task ProcessPacket(WorldSession client, CPacket packet)
        {
            return Process(client, (T) packet);
        }

        protected abstract Task Process(WorldSession client, T packet);
    }
}