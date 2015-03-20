using System;
using Chess.Core.Enums;

namespace Chess.Core.Models
{
    public class Figure
    {
        public FigureType Type { get; set; }
        public Color Color { get; set; }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Figure)obj);
        }

        protected bool Equals(Figure other)
        {
            return Type == other.Type && Color == other.Color;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ Color.GetHashCode();
            }
        }
    }
}
