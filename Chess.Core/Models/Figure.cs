using System;
using Chess.Core.Enums;
using Newtonsoft.Json;

namespace Chess.Core.Models
{
    public class Figure
    {
        public Figure()
        {

        }

        public Figure(Figure figure)
        {
            Type = figure.Type;
            Color = figure.Color;
            IsMakeFirstMove = figure.IsMakeFirstMove;
        }

        [JsonProperty("Type")]
        public FigureType Type { get; set; }
        [JsonProperty("Color")]
        public Color Color { get; set; }
        [JsonProperty("IsMakeFirstMove")]
        public bool IsMakeFirstMove { get; set; }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Figure)obj);
        }

        protected bool Equals(Figure other)
        {
            return Type == other.Type && Color == other.Color && IsMakeFirstMove == other.IsMakeFirstMove;
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
