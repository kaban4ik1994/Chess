using System;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class GameLog : Entity
    {
        public long Id { get; set; }
        public string Log { get; set; }
        public int Index { get; set; }
        public DateTime CreateDate { get; set; }
        public long GameId { get; set; }

        public Game Game { get; set; }
    }
}
