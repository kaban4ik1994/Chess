using Chess.Entities.SeedFillers;

namespace Chess.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ChessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ChessContext context)
        {
					var roleFiller = new RoleSeedFiller(context);
					roleFiller.Fill();

					//   var userFiller = new UserSeedFiller(context);
					//   userFiller.Fill();
					var playerFiller = new PlayerSeedFiller(context);
					playerFiller.Fill();

					var userRoleFiller = new UserRoleSeedFiller(context);
					userRoleFiller.Fill();

					var tokenFiller = new TokenSeedFiller(context);
					tokenFiller.Fill();

					var botFiller = new BotSeedFiller(context);
					botFiller.Fill();

					// var invitationFiller = new InvitationSeedFiller(context);
					// invitationFiller.Fill();

					base.Seed(context);
        }
    }
}
