using System.Threading.Tasks;
using Noskito.Database.Dto;
using Noskito.World.Network;
using Noskito.World.Packet.Server;

namespace Noskito.World
{
    public class WorldSession
    {
        public string Username { get; set; }
        public AccountDTO Account { get; set; }

        public NetworkClient Client { get; }

        public WorldSession(NetworkClient client)
        {
            Client = client;
        }
        
        public Task SendPacket<T>(T packet) where T : SPacket
        {
            return Client.SendPacket(packet);
        }

        public Task Disconnect()
        {
            return Client.Disconnect();
        }
    }
}