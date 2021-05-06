using System;
using System.Linq;
using DotNetty.Common.Utilities;

namespace Noskito.World.Game.Maps
{
    public class Map
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public byte[] Grid { get; init; }
        public int Height { get; init; }
        public int Width { get; init; }

        public bool IsWalkable(Position position)
        {
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            var b = Grid[4 + position.Y * Width + position.X];
            
            return b == 0 || b == 2 || (b >= 16 && b <= 19);
        }
    }
}