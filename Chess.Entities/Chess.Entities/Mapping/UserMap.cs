using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chess.Entities.Models;

namespace Chess.Entities.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(user => user.UserId);

            Property(user => user.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(user => user.Email).IsRequired();
            Property(user => user.PasswordHash).IsRequired();
            Property(user => user.Active);

            ToTable("Users");

            HasMany(entity => entity.UserRoles);
            HasMany(entity => entity.Tokens).WithRequired(item => item.User).HasForeignKey(item => item.UserId);

        }
    }
}
