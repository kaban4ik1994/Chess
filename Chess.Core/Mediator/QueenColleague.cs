using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class QueenColleague : FigureColleague, IQueenColleague
    {
        public override bool Move(Position from, Position to, IChessboard chessboard)
        {
            //TODO logic
            return true;
        }

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            throw new System.NotImplementedException();
        }
    }
}
