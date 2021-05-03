using Noskito.Enum.Authentication;

namespace Noskito.Packet.Server.Authentication
{
    public class Failc : ServerPacket
    {
        public LoginFailReason Reason { get; init; }
    }
    
    public class FailcCreator : ServerPacketCreator<Failc>
    {
        protected override string Create(Failc source)
        {
            return $"failc {(byte)source.Reason}";
        }
    }
}