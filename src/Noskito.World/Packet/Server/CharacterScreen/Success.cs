namespace Noskito.World.Packet.Server.CharacterScreen
{
    public class Success : SPacket
    {
    }

    public class SuccessCreator : SPacketCreator<Success>
    {
        protected override string CreatePacket(Success source)
        {
            return "success";
        }
    }
}