using System.Linq;
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
            var attackMoves = _moveMediator.GetExtendedAttackMovesByColor(color, chessboard);
            var possibleMoves = _moveMediator.GetExtendedPossibleMovesByColor(color, chessboard);

            var randomAttackMove = RandomGeneratorHelper.GetRandomValueFromList(attackMoves.ToList());
            var randomPossibleMove = RandomGeneratorHelper.GetRandomValueFromList(possibleMoves.ToList());

            if (randomAttackMove == null) return randomPossibleMove;
            if (randomPossibleMove == null) return randomAttackMove;

            var isAttackMove = RandomGeneratorHelper.GetRandomBoolValue();

            return isAttackMove ? randomAttackMove : randomPossibleMove;
        }
    }
}
