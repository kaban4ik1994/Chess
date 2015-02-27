using System;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Invitation : Entity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
        public long InvitatorId { get; set; }
        public long? AcceptorId { get; set; }
        public long? GameId { get; set; }

        public Player Invitator { get; set; }
        public Player Acceptor { get; set; }
        public Game Game { get; set; }
    }
}
