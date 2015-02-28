using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace Chess.Services
{
    public class InvitationService : Service<Invitation>, IInvitationService
    {
        public InvitationService(IRepositoryAsync<Invitation> repository)
            : base(repository)
        {
        }

        public Task<IEnumerable<InvitationViewModel>> GetAvailableInvitationAsync()
        {
            return Task.FromResult(Query(x => x.IsAccepted == false && x.IsDeclined == false).Select(invitation =>
                new InvitationViewModel
                {
                    CreateDate = invitation.CreateDate,
                    Id = invitation.Id,
                    InvitatorId = invitation.InvitatorId,
                    InvitatorUserName = invitation.Invitator.User.UserName
                }));
        }

        public Task<long> GetAvailableInvitationCountAsync()
        {
            return Task.FromResult(Query(x => x.IsAccepted == false && x.IsDeclined == false).Select().LongCount());
        }
    }
}
