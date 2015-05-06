using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Chess.Core.Helpers;
using Chess.Core.Models;
using Chess.Enums;

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

        private FigureColleague GetColleagueByType(FigureType figureType)
        {
            if (figureType == FigureType.Pawn)
            {
                return (PawnColleague)_pawnColleague;
            }

            if (figureType == FigureType.Queen)
            {
                return (QueenColleague)_queenColleague;
            }

            if (figureType == FigureType.Rook)
            {
                return (RookColleague)_rookColleague;
            }

            if (figureType == FigureType.King)
            {
                return (KingColleague)_kingColleague;
            }

            if (figureType == FigureType.Knight)
            {
                return (KnightColleague)_knightColleague;
            }

            if (figureType == FigureType.Bishop)
            {
                return (BishopColleague)_bishopColleague;
            }
            return null;
        }

        public MoveStatus Send(Position from, Position to, IChessboard chessboard, Color color)
        {
            var figureFrom = chessboard.GetFigureByPosition(from);
            if (figureFrom == null) return MoveStatus.Error;
            if (figureFrom.Color != color) return MoveStatus.Error;

            var result = GetColleagueByType(figureFrom.Type).Move(from, to, chessboard);

            var oppositeColor = figureFrom.Color == Color.Black ? Color.White : Color.Black;

            var isShah = CheckerGameHelper.IsShahKing(figureFrom.Color,
                GetAttackMovesByColor(oppositeColor, chessboard), chessboard);

            if (isShah)
            {
                chessboard.UndoLastMove();
                var isCheckmate = CheckerGameHelper.IsCheckmate(figureFrom.Color, chessboard, this, GetColleagueByType);
                return isCheckmate ? MoveStatus.Checkmate : MoveStatus.Shah;
            }

            return result ? MoveStatus.Success : MoveStatus.Error;
        }

        public IEnumerable<Position> GetAttackMovesByColor(Color color, IChessboard chessboard)
        {
            var cells = chessboard.Board.Cast<Cell>();
            var result = new List<Position>();
            foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
            {
                var colleague = GetColleagueByType(cell.Figure.Type);

                if (colleague != null)
                {
                    result.AddRange(colleague.GetAttackMoves(cell.Position, chessboard));
                }
            }
            return result;
        }

        public IEnumerable<Position> GetPossibleMovesByColor(Color color, IChessboard chessboard)
        {
            var cells = chessboard.Board.Cast<Cell>();
            var result = new List<Position>();
            foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
            {
                var colleague = GetColleagueByType(cell.Figure.Type);

                if (colleague != null)
                {
                    result.AddRange(colleague.GetPossibleMoves(cell.Position, chessboard));
                }
            }
            return result;
        }
    }
}
