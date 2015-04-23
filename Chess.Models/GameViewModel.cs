using System;
using Chess.Enums;

namespace Chess.Models
{
    public class GameViewModel
    {
        public long GameId { get; set; }
        public DateTime FirstPlayerGameTime { get; set; }
        public DateTime SecondPlayerGameTime { get; set; }
        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }
        public long FirstPlayerId { get; set; }
        public long SecondPlayerId { get; set; }
        public string GameLog { get; set; }
        public int LogIndex { get; set; }
        public bool IsWhiteMove { get; set; }
        public bool IsEnded { get; set; }
        public MoveStatus MoveStatus { get; set; }
    }
}
