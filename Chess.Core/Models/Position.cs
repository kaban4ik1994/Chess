using System;

namespace Chess.Core.Models
{
    public class Position
    {
        public Position()
        {

        }

        public Position(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public char X { get; set; }
        public int Y { get; set; }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Position)obj);
        }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y;
            }
        }
    }
}
