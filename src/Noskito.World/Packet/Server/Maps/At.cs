namespace Noskito.World.Packet.Server.Maps
{
    public class At : SPacket
    {
        public long CharacterId { get; init; }
        public int MapId { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public byte Direction { get; init; }
        public int Music { get; init; }
    }
    
    public class AtCreator : SPacketCreator<At>
    {
        protected override string CreatePacket(At source)
        {
            return $"at " +
                   $"{source.CharacterId} " +
                   $"{source.MapId} " +
                   $"{source.X} " +
                   $"{source.Y} " +
                   $"{source.Direction} " +
                   $"0 " +
                   $"{source.Music} " +
                   $"1 " +
                   $"-1";
        }
    }
}