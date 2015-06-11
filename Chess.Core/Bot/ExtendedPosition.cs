using System;
using Chess.Core.Models;

namespace Chess.Core.Bot
{
    public class ExtendedPosition
    {
        public Position From { get; set; }
        public Position To { get; set; }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtendedPosition)obj);
        }

        protected bool Equals(ExtendedPosition other)
        {
            return Equals(From, other.From) && Equals(To, other.To);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((From != null ? From.GetHashCode() : 0) * 397) ^ (To != null ? To.GetHashCode() : 0);
            }
        }
    }
}
