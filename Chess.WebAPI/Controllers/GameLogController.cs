using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class GameLogController : ApiController
    {
        private readonly IGameService _gameService;

        public GameLogController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(long invitationId, long logId)
        {
            var result = await _gameService.GetGameLogByInvitationIdAndLogId(invitationId, logId);
            if (result == null) return BadRequest();
            var countMove = await _gameService.GetQuantityOfMoveByInvitationId(invitationId);

            return Ok(new { GameData = result, Count = countMove });
        }
    }
}
