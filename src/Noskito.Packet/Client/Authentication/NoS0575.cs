namespace Noskito.Packet.Client.Authentication
{
    public class NoS05755 : ClientPacket
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class NoS05755Creator : ClientPacketCreator<NoS05755>
    {
        protected override NoS05755 Create(string[] arguments)
        {
            return new()
            {
                Username = arguments[1],
                Password = arguments[2]
            };
        }
    }
}