using System.Data.Entity;
using Chess.Configuration;
using Chess.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class RoleSeedFiller : AbstractSeedFiller<Role>
    {
        public RoleSeedFiller(ChessContext context)
            : base(context)
        {
        }

        protected override DbSet<Role> GetBaseList()
        {
            return Context.Roles;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.Roles.Count;
        }

        public override Role GenerateEntity(int index)
        {
            var list = Configurations.Roles;
            return new Role { Name = list[index], ObjectState = ObjectState.Added };
        }
    }
}
