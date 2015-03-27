using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public abstract class FigureColleague
    {
        public abstract bool Move(Position from, Position to, IChessboard chessboard);

        public abstract IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard);
    }
}