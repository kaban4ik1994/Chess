namespace Chess.Entities.SeedFillers
{
    public interface ISeedFiller<TModel> where TModel : class 
    {
        void Fill();

        TModel GenerateEntity(int index);
    }
}
