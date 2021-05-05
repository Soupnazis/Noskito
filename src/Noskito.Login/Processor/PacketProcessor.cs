using System;
using System.Threading.Tasks;
using Noskito.Login.Packet.Client;

namespace Noskito.Login.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task ProcessPacket(LoginSession client, CPacket packet);
    }

    public abstract class PacketProcessor<T> : IPacketProcessor where T : CPacket
    {
        public Type PacketType { get; } = typeof(T);

        public Task ProcessPacket(LoginSession client, CPacket packet)
        {
            return Process(client, (T) packet);
        }

        protected abstract Task Process(LoginSession client, T packet);
    }
}