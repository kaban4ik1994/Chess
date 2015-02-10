using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class User : Entity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}
