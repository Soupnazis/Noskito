using System.Threading.Tasks;

namespace Noskito.World.Abstraction.Network
{
    public interface IWorldServer
    {
        bool IsOnline { get; }
        
        Task Start(int port);
        Task Stop();
    }
}