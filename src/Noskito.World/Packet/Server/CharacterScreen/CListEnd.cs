namespace Noskito.World.Packet.Server.CharacterScreen
{
    public class CListEnd : SPacket
    {
    }

    public class CListEndCreator : SPacketCreator<CListEnd>
    {
        protected override string CreatePacket(CListEnd source)
        {
            return "clist_end";
        }
    }
}