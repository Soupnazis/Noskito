using System;
using System.Threading.Tasks;
using Noskito.Login.Abstraction.Network;
using Noskito.Packet.Client;
using Noskito.Packet.Server;

namespace Noskito.Login.Processor
{
    public interface ILoginProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(ILoginClient client, ClientPacket packet);
    }
    
    public abstract class LoginProcessor<T> : ILoginProcessor where T : ClientPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public Task ProcessPacket(ILoginClient client, ClientPacket packet)
        {
            return Process(client, (T) packet);
        }

        protected abstract Task Process(ILoginClient client, T packet);
    }
}