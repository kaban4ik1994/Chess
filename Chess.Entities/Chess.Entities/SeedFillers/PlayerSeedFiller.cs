using System.Data.Entity;
using Chess.Configuration;
using Chess.Entities.Models;
using Chess.Helpers;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class PlayerSeedFiller : AbstractSeedFiller<Player>
    {
        public PlayerSeedFiller(ChessContext context)
            : base(context)
        {

        }

        protected override DbSet<Player> GetBaseList()
        {
            return Context.Players;
        }

        protected override int GetGenerationCount()
        {
            return 1;
        }

        public override Player GenerateEntity(int index)
        {
            return new Player
                       {
                           User =
                               new User
                                   {
                                       Active = true,
                                       UserName = Configurations.AdminUserName,
                                       FirstName = Configurations.AdminFirstName,
                                       SecondName = Configurations.AdminSecondName,
                                       Email = Configurations.AdminEmail,
                                       PasswordHash = PasswordHashHelper.GetHash(Configurations.AdminPassword),
                                       ObjectState = ObjectState.Added
                                   },
                           IsBot = false,
                           ObjectState = ObjectState.Added
                       };
        }
    }
}
