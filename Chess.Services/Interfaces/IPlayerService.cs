using Chess.Entities.Models;
using Chess.Enums;
using Service.Pattern;

namespace Chess.Services.Interfaces
{
    public interface IPlayerService : IService<Player>
    {
        Player GetBotPlayerByBotType(BotType type);
    }
}
