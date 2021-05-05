using System;

namespace Noskito.Login.Packet.Server
{
    public abstract class SPacketCreator
    {
        public abstract Type PacketType { get; }
        public abstract string Create(SPacket source);
    }

    public abstract class SPacketCreator<T> : SPacketCreator where T : SPacket
    {
        public override Type PacketType { get; } = typeof(T);

        public override string Create(SPacket source)
        {
            return CreatePacket((T) source);
        }

        protected abstract string CreatePacket(T source);
    }
}