using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Models;
using Service.Pattern;

namespace Chess.Services.Interfaces
{
    public interface IInvitationService : IService<Invitation>
    {
        Task<IEnumerable<InvitationViewModel>> GetAvailableInvitationAsync();
        Task<long> GetAvailableInvitationCountAsync();
        Task<InvitationViewModel> AddInvitation(Invitation invitation);
        Task<bool> DeleteInvitationByInvitationIdAndUserToken(long invitationId, Guid userToken);
    }
}
