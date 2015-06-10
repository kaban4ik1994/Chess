using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Bot.Interfaces
{
    public interface IBotMediator
    {
        ExtendedPosition Send(IChessboard chessboard, Color color, BotType botType);
    }
}
