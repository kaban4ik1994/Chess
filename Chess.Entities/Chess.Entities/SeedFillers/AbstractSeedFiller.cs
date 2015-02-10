using System;
using System.Data.Entity;
using System.Linq;

namespace Chess.Entities.SeedFillers
{
    public class AbstractSeedFiller<TModel> : ISeedFiller<TModel> where TModel : class 
    {
        private readonly ChessContext _context;

        public AbstractSeedFiller(ChessContext context)
        {
            _context = context;
        }


        protected ChessContext Context
        {
            get { return _context; }
        }

        protected void GenerateData(
            IDbSet<TModel> db,
            int count,
            Func<int, TModel> generate) 
        {
            if (db.Any())
            {
                return;
            }

            const int limitPack = 100;

            for (var i = 0; i < count; i++)
            {
                db.Add(generate(i));
                if (i % limitPack == 0 && i != 0)
                {
                    EndTransaction();
                }
            }

            EndTransaction();
        }

        protected virtual void EndTransaction()
        {
            Context.SaveChanges();
        }

        protected virtual DbSet<TModel> GetBaseList()
        {
            throw new NotImplementedException("Please implement function to get base list");
        }

        protected virtual int GetGenerationCount()
        {
            throw new NotImplementedException("Please implement function to get generation count");
        }

        public void Fill()
        {
            GenerateData(GetBaseList(), GetGenerationCount(), GenerateEntity);
        }

        public virtual TModel GenerateEntity(int index)
        {
            throw new NotImplementedException("Please implement function to genarete entity");
        }
    }
}
