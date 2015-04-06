using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Core.Models;
using Chess.Enums;
using Chess.Models;
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
            var result = await _gameService.GetGameBoardByInvitationId(invitationId);
            if (result == null) return BadRequest();
            return Ok(new { GameData = result });
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]MakeMoveViewModel model)
        {
            var result =
                await
                    _gameService.MakeMove(model.GameId, new Position { X = model.FromX, Y = model.FromY },
                        new Position { X = model.ToX, Y = model.ToY });
            if (result.MoveStatus != MoveStatus.Success)
            {
                var message = string.Empty;
                if (result.MoveStatus == MoveStatus.Error) message = "Wrong move.";
                else if (result.MoveStatus == MoveStatus.Shah) message = "You to shah.";
                else if (result.MoveStatus == MoveStatus.Checkmate) message = "You to checkmate";
                return BadRequest(message);
            }
            return Ok(new { GameData = result });
        }
    }
}
