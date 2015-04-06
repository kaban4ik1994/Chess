using System.Collections.Generic;
using System.Linq;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Helpers
{
    public static class CheckerGameHelper
    {

        public static bool IsShahKing(Color color, IEnumerable<Position> attackMoves, IChessboard chessboard)
        {
            var kingPosition = GetPositionKing(color, chessboard);
            return attackMoves.FirstOrDefault(position => position.Equals(kingPosition)) != null;
        }

        public static Position GetPositionKing(Color color, IChessboard chessboard)
        {
            var cellWhiteKing = chessboard.Board.Cast<Cell>().FirstOrDefault(cell => cell.Figure != null && cell.Figure.Color == color && cell.Figure.Type == FigureType.King);
            return cellWhiteKing == null ? null : cellWhiteKing.Position;
        }
    }
}
