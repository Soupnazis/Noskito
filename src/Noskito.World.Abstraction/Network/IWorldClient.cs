using System;
using System.Threading.Tasks;
using Noskito.World.Packet.Server;

namespace Noskito.World.Abstraction.Network
{
    public interface IWorldClient
    {
        Guid Id { get; }
        int EncryptionKey { get; set; }
        int LastPacketId { get; set; }
        Task SendPacket<T>(T packet) where T : SPacket;
        Task Disconnect();
    }
}