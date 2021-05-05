namespace Noskito.World.Packet.Server.CharacterScreen
{
    public class CListStart : SPacket
    {
    }

    public class CListStartCreator : SPacketCreator<CListStart>
    {
        protected override string CreatePacket(CListStart source)
        {
            return "clist_start 0";
        }
    }
}