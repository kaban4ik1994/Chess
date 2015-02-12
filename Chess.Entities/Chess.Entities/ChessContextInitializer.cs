using System.Data.Entity;
using Chess.Entities.SeedFillers;

namespace Chess.Entities
{
    public class ChessContextInitializer : CreateDatabaseIfNotExists<ChessContext>
    {
        protected override void Seed(ChessContext context)
        {
            var roleFiller = new RoleSeedFiller(context);
            roleFiller.Fill();

            var userFiller = new UserSeedFiller(context);
            userFiller.Fill();

            var userRoleFiller = new UserRoleSeedFiller(context);
            userRoleFiller.Fill();

            var tokenFiller = new TokenSeedFiller(context);
            tokenFiller.Fill();
            base.Seed(context);
        }
    }
}
