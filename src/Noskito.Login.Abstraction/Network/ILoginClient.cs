using System;
using System.Threading.Tasks;
using Noskito.Login.Packet.Server;

namespace Noskito.Login.Abstraction.Network
{
    public interface ILoginClient
    {
        Guid Id { get; }
        Task SendPacket<T>(T packet) where T : SPacket;
        Task Disconnect();
    }
}