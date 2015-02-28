using System;
using System.Data.Entity;
using System.Linq;
using Chess.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class InvitationSeedFiller : AbstractSeedFiller<Invitation>
    {
        public InvitationSeedFiller(ChessContext context)
            : base(context)
        {
        }

        protected override DbSet<Invitation> GetBaseList()
        {
            return Context.Invitations;
        }

        protected override int GetGenerationCount()
        {
            return Context.Players.Count();
        }

        public override Invitation GenerateEntity(int index)
        {
            var players = Context.Players.ToList();

            return new Invitation
            {
                Invitator = players[index],
                CreateDate = DateTime.Now,
                IsAccepted = false,
                IsDeclined = false,
                ObjectState = ObjectState.Added
            };
        }
    }
}
