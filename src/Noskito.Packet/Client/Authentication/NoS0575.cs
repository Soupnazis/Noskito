namespace Noskito.Packet.Client.Authentication
{
    public class NoS0575 : ClientPacket
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class NoS0575Creator : ClientPacketCreator<NoS0575>
    {
        protected override NoS0575 Create(string[] arguments)
        {
            return new()
            {
                Username = arguments[1],
                Password = arguments[2]
            };
        }
    }
}