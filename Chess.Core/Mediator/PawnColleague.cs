using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class PawnColleague : FigureColleague
    {
        public PawnColleague(Mediator mediator)
            : base(mediator)
        {
        }

        public bool Send(Position from, Position to, Chessboard chessboard)
        {
            return Mediator.Send(from, to, chessboard, this);
        }

        public override bool Move(Position from, Position to, Chessboard chessboard)
        {
            var figureTo = chessboard.GetFigureByPosition(to);
            var figureFrom = chessboard.GetFigureByPosition(from);

            if (figureTo == null)
            {
             //   if(from.X==to.X && from.)
            }

            else
            {
                if (figureTo.Color == figureFrom.Color) return false;
            }

            return true;
        }
    }
}
