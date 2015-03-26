using System;

namespace Chess.Models
{
    public class InvitationViewModel
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
        public long InvitatorId { get; set; }
        public long? AcceptorId { get; set; }
        public string AcceptorUserName { get; set; }
        public long? GameId { get; set; }
        public string InvitatorUserName { get; set; }
    }
}
