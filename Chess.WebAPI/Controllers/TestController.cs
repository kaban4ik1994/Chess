using System.Threading.Tasks;
using System.Web.Http;
using Chess.Services.Interfaces;

namespace Chess.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        private readonly IUserService _userService;
        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _userService.Query().SelectAsync());
        } 
    }
}
