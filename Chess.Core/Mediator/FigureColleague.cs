namespace Chess.Core.Mediator
{
    public abstract class FigureColleague
    {
        protected Mediator Mediator;

        public FigureColleague(Mediator mediator)
        {
            Mediator = mediator;
        }

    }
}
