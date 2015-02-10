using System.Data.Entity;
using System.Linq;
using Chess.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class TokenSeedFiller : AbstractSeedFiller<Token>
    {
        public TokenSeedFiller(ChessContext context)
            : base(context)
        {
        }

        protected override DbSet<Token> GetBaseList()
        {
            return Context.Tokens;
        }

        protected override int GetGenerationCount()
        {
            return Context.Set<User>().Count();
        }

        public override Token GenerateEntity(int index)
        {
            var userList = Context.Set<User>().ToList();
            userList.ForEach(user => user.ObjectState = ObjectState.Modified);
            return new Token { User = userList[index], ObjectState = ObjectState.Added };
        }
    }
}
