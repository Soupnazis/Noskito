namespace Noskito.World.Game.Maps
{
    public class Map
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public byte[] Grid { get; init; }
        public int Height { get; init; }
        public int Width { get; init; }
    }
}