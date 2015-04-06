using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Enums;
using Chess.Core.Helpers;
using Chess.Core.Models;

namespace Chess.Core.Mediator
{
    public class MoveMediator : IMoveMediator
    {

        private readonly IPawnColleague _pawnColleague;
        private readonly IQueenColleague _queenColleague;
        private readonly IRookColleague _rookColleague;
        private readonly IKingColleague _kingColleague;
        private readonly IKnightColleague _knightColleague;
        private readonly IBishopColleague _bishopColleague;

        public MoveMediator(IPawnColleague pawnColleague, IQueenColleague queenColleague, IRookColleague rookColleague, IKingColleague kingColleague, IKnightColleague knightColleague, IBishopColleague bishopColleague)
        {
            _pawnColleague = pawnColleague;
            _queenColleague = queenColleague;
            _rookColleague = rookColleague;
            _kingColleague = kingColleague;
            _knightColleague = knightColleague;
            _bishopColleague = bishopColleague;
        }

        public bool Send(Position from, Position to, IChessboard chessboard)
        {
            var figureFrom = chessboard.GetFigureByPosition(from);
            var result = false;
            if (figureFrom == null) return false;

            if (figureFrom.Type == FigureType.Pawn)
            {
                result = _pawnColleague.Move(from, to, chessboard);
            }

            else if (figureFrom.Type == FigureType.Queen)
            {
                result = _queenColleague.Move(@from, to, chessboard);
            }

            else if (figureFrom.Type == FigureType.Rook)
            {
                result = _rookColleague.Move(@from, to, chessboard);
            }

            else if (figureFrom.Type == FigureType.King)
            {
                result = _kingColleague.Move(from, to, chessboard);
            }

            else if (figureFrom.Type == FigureType.Knight)
            {
                result = _knightColleague.Move(from, to, chessboard);
            }

            else if (figureFrom.Type == FigureType.Bishop)
            {
                result = _bishopColleague.Move(from, to, chessboard);
            }
            var isShah = CheckerGameHelper.IsShahKing(figureFrom.Color,
                GetAttackMovesByColor(figureFrom.Color == Color.Black ? Color.White : Color.Black, chessboard), chessboard);

            if (isShah)
            {
                chessboard.UndoLastMove();
                result = false;
            }

            return result;
        }

        public IEnumerable<Position> GetAttackMovesByColor(Color color, IChessboard chessboard)
        {
            var cells = chessboard.Board.Cast<Cell>();
            var result = new List<Position>();
            foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
            {
                if (cell.Figure.Type == FigureType.Pawn)
                {
                    result.AddRange(_pawnColleague.GetAttackMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Queen)
                {
                    result.AddRange(_queenColleague.GetAttackMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Rook)
                {
                    result.AddRange(_rookColleague.GetAttackMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.King)
                {
                    result.AddRange(_kingColleague.GetAttackMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Knight)
                {
                    result.AddRange(_knightColleague.GetAttackMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Bishop)
                {
                    result.AddRange(_bishopColleague.GetAttackMoves(cell.Position, chessboard));
                }
            }

            return result;
        }
    }
}
