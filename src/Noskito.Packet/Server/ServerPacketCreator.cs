using System;

namespace Noskito.Packet.Server
{
    public interface IServerPacketCreator
    {
        Type PacketType { get; }
        string CreatePacket(ServerPacket source);
    }
    
    public abstract class ServerPacketCreator<T> : IServerPacketCreator where T : ServerPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public string CreatePacket(ServerPacket source)
        {
            return Create((T) source);
        }

        protected abstract string Create(T source);
    }
}