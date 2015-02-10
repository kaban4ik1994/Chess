using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class UserRole : Entity
    {
        public long UserRoleId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
