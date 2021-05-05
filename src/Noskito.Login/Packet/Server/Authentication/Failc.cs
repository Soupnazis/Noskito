using Noskito.Enum;

namespace Noskito.Login.Packet.Server.Authentication
{
    public class Failc : SPacket
    {
        public LoginFailReason Reason { get; init; }
    }

    public class FailcCreator : SPacketCreator<Failc>
    {
        protected override string CreatePacket(Failc source)
        {
            return $"failc {(byte) source.Reason}";
        }
    }
}