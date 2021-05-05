using System.Threading.Tasks;
using Noskito.Login.Network;
using Noskito.Login.Packet.Server;

namespace Noskito.Login
{
    public class LoginSession
    {
        private readonly NetworkClient client;

        public LoginSession(NetworkClient client)
        {
            this.client = client;
        }

        public Task SendPacket<T>(T packet) where T : SPacket
        {
            return client.SendPacket(packet);
        }

        public Task Disconnect()
        {
            return client.Disconnect();
        }
    }
}