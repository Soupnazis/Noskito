using System.Threading.Tasks;
using Noskito.Enum.Authentication;
using Noskito.Login.Abstraction.Network;
using Noskito.Packet.Server.Authentication;

namespace Noskito.Login.Processor.Extension
{
    public static class LoginClientExtensions
    {
        public static Task SendLoginFail(this ILoginClient client, LoginFailReason reason)
        {
            return client.SendPacket(new Failc
            {
                Reason = reason
            });
        }
    }
}