using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Entities.Models;
using Service.Pattern;

namespace Chess.Services.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Task<Cell[,]> InitializeNewGameByInvitationId(long invitationId);
    }
}
