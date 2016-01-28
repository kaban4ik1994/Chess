using System.Threading.Tasks;
using Chess.Core.Bot;
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
		Task<GameLogViewModel> GetGameLogByInvitationIdAndLogId(long invitationId, long logId);
		Task<long> GetQuantityOfMoveByInvitationId(long invitationId);
		Task<ExtendedPosition> GetBotMove(long gameId);
		Task<bool> IsMoveOfBot(long gameId);
		Task<bool> IsGameEnded(long gameId);
	}
}
