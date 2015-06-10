using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Chess.Entities.Models;
using Chess.Services.Interfaces;
using Chess.WebAPI.Filters.AuthorizationFilters;
using Chess.WebAPI.Models;

namespace Chess.WebAPI.Controllers
{
    [CheckRole("Admin,User"), EnableCors("*", "*", "*")]
    public class BotInvitationController : ApiController
    {
        private readonly IPlayerService _playerService;
        private readonly IInvitationService _invitationService;

        public BotInvitationController(IPlayerService playerService, IInvitationService invitationService)
        {
            _playerService = playerService;
            _invitationService = invitationService;
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] AddInvitationWithBotViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var invitation = Mapper.Map<Invitation>(model);
            var player = _playerService.GetBotPlayerByBotType(model.BotType);
            invitation.AcceptorId = player.Id;
            invitation.IsAccepted = true;
            var invitationId = await _invitationService.AddInvitation(invitation);
            return Created("Invitation was created", invitationId);
        }
    }
}
