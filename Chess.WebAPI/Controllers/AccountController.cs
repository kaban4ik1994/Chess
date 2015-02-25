﻿using System;
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
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AccountController(IUserService userService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _userService = userService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]LogInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _userService.GetUserByEmailAndPasswordAsync(model.Email, model.Password));
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
