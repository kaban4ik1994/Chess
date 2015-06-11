using System.Collections.Generic;
using Chess.Core.Bot.Interfaces;
using Chess.Core.Mediator;
using Chess.Core.Models;
using Chess.Enums;
using Chess.Helpers;

namespace Chess.Core.Bot
{
    public class MonkeyBotColleague : IMonkeyBotColleague
    {
        private readonly IMoveMediator _moveMediator;

        public MonkeyBotColleague(IMoveMediator moveMediator)
        {
            _moveMediator = moveMediator;
        }

        public BotType GetBotType()
        {
            return BotType.MonkeyBot;
        }

        public ExtendedPosition GetMove(IChessboard chessboard, Color color)
        {
            var allMoves = new List<ExtendedPosition>();
            allMoves.AddRange(_moveMediator.GetExtendedAttackMovesByColor(color, chessboard));
            allMoves.AddRange(_moveMediator.GetExtendedPossibleMovesByColor(color, chessboard));

            var isCorrectMove = false;
            var randomMove = default(ExtendedPosition);

            while (!isCorrectMove)
            {
                randomMove = RandomGeneratorHelper.GetRandomValueFromList(allMoves);
                if (randomMove == null) return null;
                var moveResult = _moveMediator.Send(randomMove.From, randomMove.To, chessboard, color);

                switch (moveResult)
                {
                    case MoveStatus.Success:
                        isCorrectMove = true;
                        chessboard.UndoLastMove(); break;
                    case MoveStatus.Checkmate:
                        isCorrectMove = true; break;
                    default:
                        allMoves.Remove(randomMove);
                        break;
                }
            }

            return randomMove;
        }
    }
}
