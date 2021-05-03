using System.Threading.Tasks;

namespace Noskito.Login.Abstraction.Network
{
    public interface ILoginServer
    {
        bool IsOnline { get; }
        
        Task Start(int port);
        Task Stop();
    }
}