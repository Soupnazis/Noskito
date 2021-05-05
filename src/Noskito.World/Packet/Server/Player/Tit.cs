namespace Noskito.World.Packet.Server.Player
{
    public class Tit : SPacket
    {
        public int ClassName { get; init; }
        public string Name { get; init; }
    }

    public class TitCreator : SPacketCreator<Tit>
    {
        protected override string CreatePacket(Tit source)
        {
            return "tit " +
                   $"{source.ClassName} " +
                   $"{source.Name}";
        }
    }
}