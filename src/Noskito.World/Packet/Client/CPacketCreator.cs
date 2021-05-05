namespace Noskito.World.Packet.Client
{
    public abstract class CPacketCreator
    {
        public abstract string Header { get; }
        public abstract CPacket Create(string[] parameters);
    }

    public abstract class CPacketCreator<T> : CPacketCreator where T : CPacket
    {
        public override string Header { get; } = typeof(T).Name.ToLower();

        public override CPacket Create(string[] parameters)
        {
            return CreatePacket(parameters);
        }

        protected abstract T CreatePacket(string[] parameters);
    }
}