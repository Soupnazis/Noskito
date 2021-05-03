using System;
using System.Threading.Tasks;
using Noskito.Login.Abstraction.Network;
using Noskito.Packet.Client;

namespace Noskito.Login.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(ILoginClient client, ClientPacket packet);
    }
    
    public abstract class PacketProcessor<T> : IPacketProcessor where T : ClientPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public Task ProcessPacket(ILoginClient client, ClientPacket packet)
        {
            return Process(client, (T) packet);
        }

        protected abstract Task Process(ILoginClient client, T packet);
    }
}