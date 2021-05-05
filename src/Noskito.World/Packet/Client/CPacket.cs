namespace Noskito.World.Packet.Client
{
    /// <summary>
    ///     Represent a packet received from a client
    /// </summary>
    public abstract class CPacket
    {
        public int PacketId { get; internal set; }
    }
}