using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public abstract class Mediator : IMoveMediator
    {
        public abstract bool Send(Position from, Position to, Chessboard chessboard, FigureColleague colleague);
    }
}
