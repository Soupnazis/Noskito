using System;
using System.Threading.Tasks;
using Noskito.Database.Abstraction.Entity;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server;

namespace Noskito.World.Abstraction.Network
{
    public interface IWorldClient
    {
        Guid Id { get; }
        int EncryptionKey { get; set; }
        string Username { get; set; }
        Account Account { get; set; }
        Task SendPacket<T>(T packet) where T : SPacket;
        Task Disconnect();
        Task Process<T>(T packet) where T : CPacket;
    }
}