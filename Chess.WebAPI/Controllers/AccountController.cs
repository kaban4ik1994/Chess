using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Chess.Entities.Models;
using Chess.Models;
using Chess.Services.Interfaces;
using Chess.WebAPI.Models;
using Repository.Pattern.UnitOfWork;

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
            if (!ModelState.IsValid) return BadRequest();
            var result = await _userService.GetUserByEmailAndPasswordAsync(model.Email, model.Password);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = Mapper.Map<User>(model);
            var userId = await _userService.AddUser(user);
            return Created("User was created", userId);
        }
    }
}
