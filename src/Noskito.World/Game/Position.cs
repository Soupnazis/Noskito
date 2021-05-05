using System;

namespace Noskito.World.Game
{
    public readonly struct Position : IEquatable<Position>
    {
        public int X { get; init; }
        public int Y { get; init; }
        
        public bool Equals(Position other)
        {
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position && Equals((Position)obj);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}