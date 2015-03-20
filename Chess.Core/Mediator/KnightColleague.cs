using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class KnightColleague : FigureColleague
    {
        public KnightColleague(Mediator mediator) : base(mediator)
        {
        }

        public bool Send(Position from, Position to, Chessboard chessboard)
        {
            return Mediator.Send(from, to, chessboard, this);
        }

        public override bool Move(Position from, Position to, Chessboard chessboard)
        {
            //TODO logic
            return true;
        }
    }
}
