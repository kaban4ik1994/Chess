using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Models;
using Service.Pattern;

namespace Chess.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Task<GameViewModel> GetGameBoardByInvitationId(long invitationId);
        Task<GameViewModel> MakeMove(long gameId, Position from, Position to);
    }
}
