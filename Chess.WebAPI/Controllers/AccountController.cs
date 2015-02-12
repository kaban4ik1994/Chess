using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Chess.Models;
using Chess.Services.Interfaces;

namespace Chess.WebAPI.Controllers
{
    [AllowAnonymous, EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]LogInViewModel model)
        {
            return Ok(await _userService.GetUserByEmailAndPasswordAsync(model.Email, model.Password));
        }
    }
}
