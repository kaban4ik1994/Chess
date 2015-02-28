using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Chess.Entities.Models;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;
using Chess.WebAPI.Models;
using Repository.Pattern.UnitOfWork;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class InvitationController : ApiController
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] AddInvitationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var invitation = Mapper.Map<Invitation>(model);
            var invitationId = await _invitationService.AddInvitation(invitation);
            return Created("Invitation was created", invitationId);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long invitationId)
        {
            var token = HttpContext.Current.Request.Headers.Get("Authorization");
            return null;
        }
    }
}
