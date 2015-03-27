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
        Task<IEnumerable<InvitationViewModel>> GetAvailableInvitationsAsync();
        Task<IEnumerable<InvitationViewModel>> GetAcceptInvitationsByUserTokenAsync(Guid userToken);
        Task<IEnumerable<InvitationViewModel>> GetClosedInvitationsByUserTokenAsync(Guid userToken);
        Task<long> GetAvailableInvitationsCountAsync();
        Task<long> GetAcceptInvitationsCountByUserTokenAsync(Guid userToken);
        Task<long> GetClosedInvitationsCountByUserTokenAsync(Guid userToken);
        Task<InvitationViewModel> AddInvitation(Invitation invitation);
        Task<bool> DeleteInvitationByInvitationIdAndUserToken(long invitationId, Guid userToken);
        Task<bool> AcceptInvitation(long invitationId, Guid userToken);
    }
}
