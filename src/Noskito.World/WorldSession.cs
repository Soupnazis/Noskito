using System;
using System.Threading.Tasks;
using Noskito.Database.Dto;
using Noskito.World.Game.Entities;
using Noskito.World.Network;
using Noskito.World.Packet.Server;

namespace Noskito.World
{
    public class WorldSession
    {
        private readonly NetworkClient client;

        public WorldSession(NetworkClient client)
        {
            this.client = client;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public AccountDTO Account { get; set; }
        public Character Character { get; set; }

        public int Key
        {
            get => client.EncryptionKey;
            set => client.EncryptionKey = value;
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