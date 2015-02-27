using System;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class PlayerGame : Entity
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public DateTime LookTime { get; set; }

        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
