using Chess.Enums;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class DebutGame : Entity
    {
        public long Id { get; set; }
        public string Log { get; set; }

        public string NextMove { get; set; }

        public double WhiteWinPercent { get; set; }
        public double BlackWinPercent { get; set; }
        public long TotalBlackWinGames { get; set; }
        public long TotalWhiteWinGames { get; set; }
        public long TotalGame { get; set; }
        public Color MoveColor { get; set; }
    }
}
