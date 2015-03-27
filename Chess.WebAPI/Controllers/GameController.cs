using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Core.Models;
using Chess.Entities.Models;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(long invitationId)
        {
            return Ok(new {GameData = await _gameService.GetGameBoardByInvitationId(invitationId)});
        }
    }
}
