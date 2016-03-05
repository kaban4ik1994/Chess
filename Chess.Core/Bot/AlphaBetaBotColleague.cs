using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Bot.Interfaces;
using Chess.Core.Helpers;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Bot
{
	public class AlphaBetaBotColleague : IAlphaBetaBotColleague
	{
		private readonly IMoveMediator _moveMediator;

		public AlphaBetaBotColleague(IMoveMediator moveMediator)
		{
			_moveMediator = moveMediator;
		}

		public BotType GetBotType()
		{
			return BotType.AlphaBetaBot;
		}

		public ExtendedPosition GetMove(IChessboard chessboard, Color color)
		{
			_currentColor = color;
			var algorithmResult = AlphaBeta(2, chessboard, color, ChessConstants.MinCostValue, ChessConstants.MaxCostValue);

			return algorithmResult.Move;
		}

		private Color _currentColor;

		private AlgorithmResultModel AlphaBeta(int depth, IChessboard chessboard, Color color, int alpha, int beta)
		{
			var oppositeColor = chessboard.GetOppositeColor(color);
			var allMoves = new Stack<ExtendedPosition>(_moveMediator.GetAllMoves(color, chessboard));

			var isMaximizer = color == _currentColor;
			if (depth == 0)
			{
				var opponentAttackMoves = _moveMediator.GetAttackMovesByColor(oppositeColor, chessboard).ToList();
				var opponentLegalMoves = _moveMediator.GetPossibleMovesByColor(oppositeColor, chessboard).ToList();
				var legalMoves = _moveMediator.GetPossibleMovesByColor(color, chessboard).ToList();
				var attackMoves = _moveMediator.GetAttackMovesByColor(color, chessboard).ToList();
				var currentCost =
					chessboard.Evaluation(color, legalMoves.Count(), attackMoves.Count(),
						CheckerGameHelper.IsShahKing(oppositeColor, attackMoves, chessboard),
						CheckerGameHelper.IsCheckmate(oppositeColor, chessboard, _moveMediator)) -
					chessboard.Evaluation(oppositeColor, opponentLegalMoves.Count(), opponentAttackMoves.Count,
						CheckerGameHelper.IsShahKing(color, opponentAttackMoves, chessboard),
						CheckerGameHelper.IsCheckmate(color, chessboard, _moveMediator));

				return new AlgorithmResultModel(currentCost);
			}

			AlgorithmResultModel returnMove = new AlgorithmResultModel();
			AlgorithmResultModel bestMove = null;

			if (isMaximizer)
			{
				while (allMoves.Count > 0)
				{
					var currentMove = allMoves.Pop();
					var moveResult = _moveMediator.Send(currentMove.From, currentMove.To, chessboard, color);
					if (moveResult != MoveStatus.Success) continue;
					returnMove = AlphaBeta(depth - 1, chessboard, oppositeColor, alpha, beta);
					if (moveResult == MoveStatus.Success)
						chessboard.UndoLastMove();

					if ((bestMove == null) || (bestMove.CurrentCosts < returnMove.CurrentCosts))
					{
						bestMove = returnMove;
						bestMove.Move = currentMove;
					}
					if (returnMove.CurrentCosts > alpha)
					{
						alpha = returnMove.CurrentCosts;
						bestMove = returnMove;
					}
					if (beta <= alpha)
					{
						bestMove.CurrentCosts = beta;
						bestMove.Move = null;
						return bestMove;
					}
				}
				return bestMove;
			}
			else
			{
				while (allMoves.Count > 0)
				{
					var currentMove = allMoves.Pop();
					var moveResult = _moveMediator.Send(currentMove.From, currentMove.To, chessboard, color);
					if (moveResult != MoveStatus.Success) continue;
					returnMove = AlphaBeta(depth - 1, chessboard, oppositeColor, alpha, beta);

					if (moveResult == MoveStatus.Success) chessboard.UndoLastMove();

					if ((bestMove == null) || (bestMove.CurrentCosts > returnMove.CurrentCosts))
					{
						bestMove = returnMove;
						bestMove.Move = currentMove;
					}
					if (returnMove.CurrentCosts < beta)
					{
						beta = returnMove.CurrentCosts;
						bestMove = returnMove;
					}
					if (beta <= alpha)
					{
						bestMove.CurrentCosts = alpha;
						bestMove.Move = null;
						return bestMove;
					}
				}
			}
			return bestMove;
		}
	}
}
