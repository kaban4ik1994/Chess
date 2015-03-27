using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Core.Models;
using Chess.WebAPI.Filters.AuthorizationFilters;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class GameController : ApiController
    {
        private readonly IChessboard _chessboard;

        public GameController(IChessboard chessboard)
        {
            _chessboard = chessboard;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            _chessboard.InitNewGame();
            return Json(_chessboard.Board);
        }
    }
}
