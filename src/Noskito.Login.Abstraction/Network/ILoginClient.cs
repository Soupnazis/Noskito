using System;
using System.Threading.Tasks;
using Noskito.Packet.Server;

namespace Noskito.Login.Abstraction.Network
{
    public interface ILoginClient
    {
        Guid Id { get; }
        Task SendPacket(ServerPacket packet);
        Task Disconnect();
    }
}