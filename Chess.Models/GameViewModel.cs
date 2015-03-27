using System;

namespace Chess.Models
{
    public class GameViewModel
    {
        public long GameId { get; set; }
        public DateTime FirstPlayerGameTime { get; set; }
        public DateTime SecondPlayerGameTime { get; set; }
        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }
        public string GameLog { get; set; }
        public int LogIndex { get; set; }
        public bool IsWhiteMove { get; set; }
    }
}
