using System;
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
        private readonly IRepositoryAsync<User> _repositoryAsync;

        public InvitationService(IRepositoryAsync<Invitation> repository, IUnitOfWorkAsync unitOfWorkAsync, IRepositoryAsync<User> repositoryAsync)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _repositoryAsync = repositoryAsync;
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

        public async Task<bool> DeleteInvitationByInvitationIdAndUserToken(long invitationId, Guid userToken)
        {
            var isAdmin = await Task.FromResult(_repositoryAsync.Query(user1 => user1.Tokens.Any(token1 => token1.TokenData == userToken) && user1.Active)
              .Include(user1 => user1.UserRoles)
              .Include(user1 => user1.UserRoles.Select(role => role.Role))
              .Select()
              .Any(user1 => user1.UserRoles.Any(role => role.Role.Name == "Admin")));

            if (isAdmin)
            {
                Delete(invitationId);
            }

            else
            {
                var invitation = Query(x =>
                    x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
                    && x.Id == invitationId).Select().FirstOrDefault();
                if (invitation == null) return await Task.FromResult(false);
                Delete(invitation.Id);
            }

            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
