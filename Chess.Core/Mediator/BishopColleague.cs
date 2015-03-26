using System.Collections.Generic;
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

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, Chessboard chessboard)
        {
            throw new System.NotImplementedException();
        }
    }
}
