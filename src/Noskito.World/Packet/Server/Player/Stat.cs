namespace Noskito.World.Packet.Server.Player
{
    public class Stat : SPacket
    {
        public int Hp { get; init; }
        public int MaxHp { get; init; }
        public int Mp { get; init; }
        public int MaxMp { get; init; }
        public int Options { get; init; }
    }

    public class StatCreator : SPacketCreator<Stat>
    {
        protected override string CreatePacket(Stat source)
        {
            return "stat " +
                   $"{source.Hp} " +
                   $"{source.MaxHp} " +
                   $"{source.Mp} " +
                   $"{source.MaxMp} " +
                   "0 " +
                   $"{source.Options}";
        }
    }
}