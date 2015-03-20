using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public abstract class FigureColleague
    {
        protected Mediator Mediator;

        public FigureColleague(Mediator mediator)
        {
            Mediator = mediator;
        }

        public abstract bool Move(Position from, Position to, Chessboard chessboard);
    }
}
