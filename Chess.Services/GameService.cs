using System.Threading.Tasks;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace Chess.Services
{
    public class GameService : Service<Game>, IGameService
    {
        public GameService(IRepositoryAsync<Game> repository) : base(repository)
        {
        }

        public Task<Chessboard> InitializeANewGameByInvitationId(long invitationId)
        {
            throw new System.NotImplementedException();
        }
    }
}
