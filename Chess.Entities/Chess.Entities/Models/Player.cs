using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Player : Entity
    {
        public long Id { get; set; }
        public Guid IdentityNumber { get; set; }
        public bool IsBot { get; set; }


        public Bot Bot { get; set; }
        public User User { get; set; }
        public List<Invitation> Invitations { get; set; }
        public List<Invitation> Acceptions { get; set; }
        public List<PlayerGame> WatchedGames { get; set; } 
    }
}
