using System;

namespace Noskito.World.Game
{
    public readonly struct Position : IEquatable<Position>
    {
        private static readonly double Sqrt2 = Math.Sqrt(2);
        
        public int X { get; init; }
        public int Y { get; init; }

        public double GetDistance(Position position)
        {
            var iDx = Math.Abs(X - position.X);
            var iDy = Math.Abs(Y - position.Y);
            var min = Math.Min(iDx, iDy);
            var max = Math.Max(iDx, iDy);
            return min * Sqrt2 + max - min;
        }

        public bool Equals(Position other)
        {
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position && Equals((Position) obj);
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