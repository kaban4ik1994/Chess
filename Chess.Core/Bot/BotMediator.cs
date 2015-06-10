using System.Collections.Generic;
using Chess.Core.Bot.Interfaces;
using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Bot
{
    public class BotMediator : IBotMediator
    {
        private readonly IDictionary<BotType, IBotColleague> _botColleagues;

        public BotMediator(IMonkeyBotColleague monkeyBotColleague)
        {
            _botColleagues = new Dictionary<BotType, IBotColleague>
            {
                { monkeyBotColleague.GetBotType(), monkeyBotColleague }
            };

        }

        public ExtendedPosition Send(IChessboard chessboard, Color color, BotType botType)
        {
            return _botColleagues[botType].GetMove(chessboard, color);
        }
    }
}
