namespace Noskito.Packet.Client
{
    public interface IClientPacketCreator
    {
        string Header { get; }
        ClientPacket CreatePacket(string[] arguments);
    }
    
    public abstract class ClientPacketCreator<T> : IClientPacketCreator where T : ClientPacket
    {
        public virtual string Header { get; } = typeof(T).Name;
        
        public ClientPacket CreatePacket(string[] arguments)
        {
            return Create(arguments);
        }

        protected abstract T Create(string[] arguments);
    }
}