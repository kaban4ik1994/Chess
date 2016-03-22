using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
  public partial class Game: Entity
    {
        public long Id { get; set; }
        public bool IsEnded { get; set; }
        public DateTime GameStartDate { get; set; }
        public Guid GameIdentificator { get; set; }
        public DateTime FirstPlayerGameTime { get; set; }
        public DateTime SecondPlayerGameTime { get; set; }
        public GameResult Result { get; set; }

        public Invitation Invitation { get; set; }
        public List<PlayerGame> Watchers { get; set; }
        public List<GameLog> GameLogs { get; set; } 
    }
}
