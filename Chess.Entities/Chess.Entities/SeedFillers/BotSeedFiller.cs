using System;
using System.Data.Entity;
using Chess.Configuration;
using Chess.Entities.Models;
using Chess.Enums;
using Repository.Pattern.Infrastructure;

namespace Chess.Entities.SeedFillers
{
    public class BotSeedFiller : AbstractSeedFiller<Bot>
    {
        public BotSeedFiller(ChessContext context)
            : base(context)
        {
        }

        protected override DbSet<Bot> GetBaseList()
        {
            return Context.Bots;
        }

        protected override int GetGenerationCount()
        {
            return Enum.GetNames(typeof(BotType)).Length;
        }

        public override Bot GenerateEntity(int index)
        {
            return new Bot
            {
                Type = (BotType)index,
                ObjectState = ObjectState.Added,
                Player = new Player
                {
                    IsBot = true,
                    ObjectState = ObjectState.Added
                }
            };


        }
    }
}
