using System;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Token : Entity
    {
        public long TokenId { get; set; }
        public Guid TokenData { get; set; }
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
