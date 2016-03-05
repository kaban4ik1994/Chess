using System.Collections.Generic;
using System.Linq;
using Chess.Core.Bot;
using Chess.Core.Helpers;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Mediator
{
	public class MoveMediator : IMoveMediator
	{
		public IDictionary<FigureType, IFigureColleague> FigureColleagues { get; private set; }

		public MoveMediator(IBishopColleague bishopColleague, IKingColleague kingColleague, IKnightColleague knightColleague, IPawnColleague pawnColleague, IQueenColleague queenColleague, IRookColleague rookColleague)
		{
			FigureColleagues = new Dictionary<FigureType, IFigureColleague>
            {
                {bishopColleague.GetColleagueType(), bishopColleague},
                {kingColleague.GetColleagueType(), kingColleague},
                {knightColleague.GetColleagueType(), knightColleague},
                {pawnColleague.GetColleagueType(), pawnColleague},
                {queenColleague.GetColleagueType(), queenColleague},
                {rookColleague.GetColleagueType(), rookColleague}
            };
		}


		public MoveStatus Send(Position from, Position to, IChessboard chessboard, Color color)
		{
			var figureFrom = chessboard.GetFigureByPosition(from);
			if (figureFrom == null) return MoveStatus.Error;
			if (figureFrom.Color != color) return MoveStatus.Error;

			var result = FigureColleagues[figureFrom.Type].Move(from, to, chessboard);

			var oppositeColor = figureFrom.Color == Color.Black ? Color.White : Color.Black;

			var isShah = CheckerGameHelper.IsShahKing(figureFrom.Color,
					GetAttackMovesByColor(oppositeColor, chessboard), chessboard);

			if (isShah)
			{
				chessboard.UndoLastMove();
				var isCheckmate = CheckerGameHelper.IsCheckmate(figureFrom.Color, chessboard, this);
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
				var colleague = FigureColleagues[cell.Figure.Type];

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
				var colleague = FigureColleagues[cell.Figure.Type];

				if (colleague != null)
				{
					result.AddRange(colleague.GetPossibleMoves(cell.Position, chessboard));
				}
			}
			return result;
		}

		public IEnumerable<ExtendedPosition> GetExtendedAttackMovesByColor(Color color, IChessboard chessboard)
		{
			var cells = chessboard.Board.Cast<Cell>();
			var result = new List<ExtendedPosition>();
			foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
			{
				var colleague = FigureColleagues[cell.Figure.Type];

				if (colleague != null)
				{
					var attackMoves = colleague.GetAttackMoves(cell.Position, chessboard).ToList();
					attackMoves.ForEach(position => result.Add(new ExtendedPosition { From = cell.Position, To = position }));
				}
			}
			return result;
		}

		public IEnumerable<ExtendedPosition> GetAllMoves(Color color, IChessboard chessboard)
		{
			var cells = chessboard.Board.Cast<Cell>();
			var result = new List<ExtendedPosition>();
			foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
			{
				var colleague = FigureColleagues[cell.Figure.Type];

				if (colleague != null)
				{
					var attackMoves = colleague.GetAttackMoves(cell.Position, chessboard).ToList();
					attackMoves.ForEach(position => result.Add(new ExtendedPosition { From = cell.Position, To = position }));
					var possibleMoves = colleague.GetPossibleMoves(cell.Position, chessboard).ToList();
					possibleMoves.ForEach(position => result.Add(new ExtendedPosition { From = cell.Position, To = position }));
				}
			}
			return result;
		}

		public IEnumerable<ExtendedPosition> GetExtendedPossibleMovesByColor(Color color, IChessboard chessboard)
		{
			var cells = chessboard.Board.Cast<Cell>();
			var result = new List<ExtendedPosition>();
			foreach (var cell in cells.Where(cell => cell.Figure != null && cell.Figure.Color == color))
			{
				var colleague = FigureColleagues[cell.Figure.Type];

				if (colleague != null)
				{
					var possibleMoves = colleague.GetPossibleMoves(cell.Position, chessboard).ToList();
					possibleMoves.ForEach(position => result.Add(new ExtendedPosition { From = cell.Position, To = position }));
				}
			}
			return result;
		}
	}
}
