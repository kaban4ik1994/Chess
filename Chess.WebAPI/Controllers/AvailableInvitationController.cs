﻿using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;
using Repository.Pattern.UnitOfWork;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class AvailableInvitationController : ApiController
    {
        private readonly IInvitationService _invitationService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AvailableInvitationController(IUnitOfWorkAsync unitOfWorkAsync, IInvitationService invitationService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _invitationService = invitationService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new
            {
                Items = await _invitationService.GetAvailableInvitationAsync(),
                Count = await _invitationService.GetAvailableInvitationCountAsync()
            });
        }
    }
}