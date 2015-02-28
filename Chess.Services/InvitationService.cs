using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Chess.Services
{
    public class InvitationService : Service<Invitation>, IInvitationService
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public InvitationService(IRepositoryAsync<Invitation> repository, IUnitOfWorkAsync unitOfWorkAsync)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
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

        public async Task<long> AddInvitation(Invitation invitation)
        {
            Insert(invitation);
            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(invitation.Id);
        }

        public Task<bool> DeleteInvitationByInvitationIdAndUserId(long invitationId, long userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
