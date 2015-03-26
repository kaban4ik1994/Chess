using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Chess.Services.Interfaces;
using Chess.WebAPI.Helpers;
using Repository.Pattern.UnitOfWork;

namespace Chess.WebAPI.Controllers
{
    public class AcceptInvitationController : ApiController
    {
        private readonly IInvitationService _invitationService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AcceptInvitationController(IUnitOfWorkAsync unitOfWorkAsync, IInvitationService invitationService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _invitationService = invitationService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(new
            {
                Items = await _invitationService.GetAcceptInvitationByUserToken(TokenHelper.GetCurrentUserToken(HttpContext.Current)),
                Count = await _invitationService.GetAcceptInvitationCountByUserToken(TokenHelper.GetCurrentUserToken(HttpContext.Current))
            });
        }
    }
}
