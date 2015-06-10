using Chess.Core.Models;
using Chess.Enums;

namespace Chess.Core.Bot.Interfaces
{
    public interface IBotColleague
    {
        BotType GetBotType();
        ExtendedPosition GetMove(IChessboard chessboard, Color color);
    }
}
