using System.Data.Entity;
using Chess.Configuration;
using Chess.Entities.Models;
using Chess.Helpers;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class UserSeedFiller : AbstractSeedFiller<User>
    {
        public UserSeedFiller(ChessContext context)
            : base(context)
        {
        }

        protected override DbSet<User> GetBaseList()
        {
            return Context.Users;
        }

        protected override int GetGenerationCount()
        {
            return 1;
        }

        public override User GenerateEntity(int index)
        {
            return new User
                       {
                           Active = true,
                           Email = Configurations.AdminEmail,
                           PasswordHash = PasswordHashHelper.GetHash(Configurations.AdminPassword),
                           ObjectState = ObjectState.Added
                       };
        }
    }
}
