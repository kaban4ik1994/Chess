using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class BishopColleague : FigureColleague, IBishopColleague
    {
        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetPossibleMovesOnTheLineXAtDownAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtDownAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtUpAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtUpAndYAtUp(figurePosition, chessboard));

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetAttackMovesOnTheLineXAtDownAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtDownAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtUpAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtUpAndYAtUp(figurePosition, chessboard));

            return result;
        }
    }
}
