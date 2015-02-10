using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Chess.Entities.Models
{
    public partial class Role : Entity
    {
        public long RoleId { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
