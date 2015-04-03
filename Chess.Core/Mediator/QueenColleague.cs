using System.Collections.Generic;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class QueenColleague : FigureColleague, IQueenColleague
    {

        public override IEnumerable<Position> GetPossibleMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetPossibleMovesOnTheLineXAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtDownAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtDownAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtUpAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineXAtUpAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineYAtDown(figurePosition, chessboard));
            result.AddRange(GetPossibleMovesOnTheLineYAtUp(figurePosition, chessboard));

            return result;
        }

        public override IEnumerable<Position> GetAttackMoves(Position figurePosition, IChessboard chessboard)
        {
            var result = new List<Position>();

            result.AddRange(GetAttackMovesOnTheLineXAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtDownAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtDownAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtUpAndYAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineXAtUpAndYAtUp(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineYAtDown(figurePosition, chessboard));
            result.AddRange(GetAttackMovesOnTheLineYAtUp(figurePosition, chessboard));

            return result;
        }
    }
}
