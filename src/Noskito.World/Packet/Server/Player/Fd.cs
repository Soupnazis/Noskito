namespace Noskito.World.Packet.Server.Player
{
    public class Fd : SPacket
    {
        public long Reputation { get; init; }
        public int RepucationIcon { get; init; }
        public int Dignity { get; init; }
        public int DignityIcon { get; init; }
    }
    
    public class FdCreator : SPacketCreator<Fd>
    {
        protected override string CreatePacket(Fd source)
        {
            return $"fd " +
                   $"{source.Reputation} " +
                   $"{source.RepucationIcon} " +
                   $"{source.Dignity} " +
                   $"{source.DignityIcon}";
        }
    }
}