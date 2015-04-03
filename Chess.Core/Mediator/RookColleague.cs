using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class RookColleague : FigureColleague, IRookColleague
    {
        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetPossibleMovesOnTheLineXAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineYAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineYAtDown(figurePosition, chessboard));

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetAttackMovesOnTheLineXAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineYAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineYAtDown(figurePosition, chessboard));

            return result;
        }
    }
}
