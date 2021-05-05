namespace Noskito.World.Packet.Server.CharacterScreen
{
    public class Ok : SPacket
    {
    }

    public class OkCreator : SPacketCreator<Ok>
    {
        protected override string CreatePacket(Ok source)
        {
            return "OK";
        }
    }
}