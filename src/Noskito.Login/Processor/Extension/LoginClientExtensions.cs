using System.Threading.Tasks;
using Noskito.Enum;
using Noskito.Login.Packet.Server.Authentication;

namespace Noskito.Login.Processor.Extension
{
    public static class LoginClientExtensions
    {
        public static Task SendLoginFail(this LoginSession session, LoginFailReason reason)
        {
            return session.SendPacket(new Failc
            {
                Reason = reason
            });
        }
    }
}