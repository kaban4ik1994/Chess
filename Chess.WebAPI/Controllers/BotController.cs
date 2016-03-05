using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;

namespace Chess.WebAPI.Controllers
{
	[CheckRole("Admin,User"), EnableCors("*", "*", "*")]
	public class BotController : ApiController
	{
		private readonly IGameService _gameService;

		public BotController(IGameService gameService)
		{
			_gameService = gameService;
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get(long invitationId)
		{

			var botMove = await _gameService.GetBotMove(invitationId);
			await _gameService.MakeMove(invitationId, botMove.From, botMove.To);
			return Ok(new { GameData = _gameService.MakeMove(invitationId, botMove.From, botMove.To) });
		}
	}
}
