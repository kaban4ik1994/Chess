using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class QueenColleague : FigureColleague, IQueenColleague
    {
        public override bool Move(Position from, Position to, Chessboard chessboard)
        {
            //TODO logic
            return true;
        }
    }
}
