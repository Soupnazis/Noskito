namespace Noskito.Login.Packet.Client
{
    public class UnresolvedPacket : CPacket
    {
        public string Header { get; init; }
        public string[] Parameters { get; init; }
    }
}