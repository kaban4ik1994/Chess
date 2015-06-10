using System.Linq;
using Chess.Entities.Models;
using Chess.Enums;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace Chess.Services
{
    public class PlayerService : Service<Player>, IPlayerService
    {
        public PlayerService(IRepositoryAsync<Player> repository)
            : base(repository)
        {
        }

        public Player GetBotPlayerByBotType(BotType type)
        {
           return Query(player => player.IsBot && player.Bot.Type == type)
                .Include(player => player.Bot)
                .Select()
                .FirstOrDefault();
        }
    }
}
