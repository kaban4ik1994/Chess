using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            HasKey(userRole => userRole.UserRoleId);

            Property(userRole => userRole.UserRoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("UserRoles");

            HasRequired(entity => entity.Role)
                .WithMany(role => role.UserRoles)
                .HasForeignKey(userRole => userRole.RoleId);
            HasRequired(entity => entity.User)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(userRole => userRole.UserId);
        }
    }
}
