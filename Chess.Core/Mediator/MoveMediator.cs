using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Position> GetPossibleMovesByColor(Color color, IChessboard chessboard)
        {
            var cells = chessboard.Board.Cast<Cell>();
            var result = new List<Position>();
            foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
            {
                if (cell.Figure.Type == FigureType.Pawn)
                {
                    result.AddRange(_pawnColleague.GetPossibleMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Queen)
                {
                    result.AddRange(_queenColleague.GetPossibleMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Rook)
                {
                    result.AddRange(_rookColleague.GetPossibleMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.King)
                {
                    result.AddRange(_kingColleague.GetPossibleMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Knight)
                {
                    result.AddRange(_knightColleague.GetPossibleMoves(cell.Position, chessboard));
                }

                else if (cell.Figure.Type == FigureType.Bishop)
                {
                    result.AddRange(_bishopColleague.GetPossibleMoves(cell.Position, chessboard));
                }
            }

            return result;
        }
    }
}
