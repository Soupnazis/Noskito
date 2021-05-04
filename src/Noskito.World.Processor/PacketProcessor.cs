using System;
using System.Threading.Tasks;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client;

namespace Noskito.World.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(IWorldClient client, CPacket packet);
    }
    
    public abstract class PacketProcessor<T> : IPacketProcessor where T : CPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public Task ProcessPacket(IWorldClient client, CPacket packet)
        {
            return Process(client, (T) packet);
        }

        protected abstract Task Process(IWorldClient client, T packet);
    }
}