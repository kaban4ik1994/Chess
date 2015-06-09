using System.Collections.Generic;
using System.Linq;
using Chess.Core.Mediator;
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

        public static bool IsCheckmate(Color color, IChessboard chessboard,
           IMoveMediator moveMediator, IDictionary<FigureType, IFigureColleague> figureColleagues)
        {
            var cellOfFigures = chessboard.GetCellOfFiguresByColor(color);
            var isCheckmate = true;
            foreach (var cellOfFigure in cellOfFigures)
            {
                var colleague = figureColleagues[cellOfFigure.Figure.Type];
                var movesOfFigures = new List<Position>();

                movesOfFigures.AddRange(colleague.GetAttackMoves(cellOfFigure.Position, chessboard));
                movesOfFigures.AddRange(colleague.GetPossibleMoves(cellOfFigure.Position, chessboard));
                foreach (var move in movesOfFigures)
                {
                    colleague.Move(cellOfFigure.Position, move, chessboard);
                    if (IsShahKing(color,
                        moveMediator.GetAttackMovesByColor(color == Color.Black ? Color.White : Color.Black, chessboard), chessboard))
                    {
                        chessboard.UndoLastMove();
                    }
                    else
                    {
                        isCheckmate = false;
                        chessboard.UndoLastMove();
                        break;
                    }
                }

                if (!isCheckmate) break;
            }
            return isCheckmate;
        }

        public static Position GetPositionKing(Color color, IChessboard chessboard)
        {
            var cellWhiteKing = chessboard.Board.Cast<Cell>().FirstOrDefault(cell => cell.Figure != null && cell.Figure.Color == color && cell.Figure.Type == FigureType.King);
            return cellWhiteKing == null ? null : cellWhiteKing.Position;
        }
    }
}
