using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class BishopColleague : FigureColleague, IBishopColleague
    {
        public override bool Move(Position from, Position to, Chessboard chessboard)
        {
            //TODO logic
           return true;
        }
    }
}
