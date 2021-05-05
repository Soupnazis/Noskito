namespace Noskito.Login.Packet.Client.Authentication
{
    public class NoS0575 : CPacket
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class NoS0575Creator : CPacketCreator<NoS0575>
    {
        protected override NoS0575 CreatePacket(string[] parameters)
        {
            return new()
            {
                Username = parameters[1],
                Password = parameters[2]
            };
        }
    }
}