using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Infrastructure;
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

        public Task<IEnumerable<InvitationViewModel>> GetAvailableInvitationsAsync()
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

        public Task<IEnumerable<InvitationViewModel>> GetAcceptInvitationsByUserTokenAsync(Guid userToken)
        {
            var aaa = Query(x => x.IsAccepted && x.IsDeclined == false && ((x.Game == null) || (x.Game != null && x.Game.IsEnded == false)))
                .Select();
            return Task.FromResult(Query(x => x.IsAccepted && x.IsDeclined == false && ((x.Game == null) || (x.Game != null && x.Game.IsEnded == false))
               && (x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
               || x.Acceptor.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null))
                .Select(invitation =>
                new InvitationViewModel
                {
                    CreateDate = invitation.CreateDate,
                    Id = invitation.Id,
                    InvitatorId = invitation.InvitatorId,
                    InvitatorUserName = invitation.Invitator.User.UserName
                }));
        }

        public Task<IEnumerable<InvitationViewModel>> GetClosedInvitationsByUserTokenAsync(Guid userToken)
        {
            return Task.FromResult(Query(x => x.IsAccepted && x.Game.IsEnded && (x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
             || x.Acceptor.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null))
              .Select(invitation =>
              new InvitationViewModel
              {
                  CreateDate = invitation.CreateDate,
                  Id = invitation.Id,
                  InvitatorId = invitation.InvitatorId,
                  InvitatorUserName = invitation.Invitator.User.UserName
              }));
        }

        public Task<long> GetAvailableInvitationsCountAsync()
        {
            return Task.FromResult(Query(x => x.IsAccepted == false && x.IsDeclined == false).Select().LongCount());
        }

        public Task<long> GetAcceptInvitationsCountByUserTokenAsync(Guid userToken)
        {
            return
                Task.FromResult(
                    Query(
                        x =>
                            x.IsAccepted && x.IsDeclined == false && ((x.Game == null) || (x.Game != null && x.Game.IsEnded == false)) &&
                            (x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
                            || x.Acceptor.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null))
                        .Select().LongCount());

        }

        public Task<long> GetClosedInvitationsCountByUserTokenAsync(Guid userToken)
        {
            return
            Task.FromResult(
                Query(
                    x =>
                        x.IsAccepted && x.Game.IsEnded &&
                        (x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
                        || x.Acceptor.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null))
                    .Select().LongCount());
        }

        public async Task<InvitationViewModel> AddInvitation(Invitation invitation)
        {
            Insert(invitation);
            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(Query(item => invitation.Id == item.Id).Select(result => new InvitationViewModel
             {
                 CreateDate = result.CreateDate,
                 Id = result.Id,
                 InvitatorId = result.InvitatorId,
                 InvitatorUserName = result.Invitator.User.UserName
             }).FirstOrDefault());
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
                var invitation = Query(x => x.Id == invitationId && !x.IsAccepted).Select().FirstOrDefault();
                if (invitation == null) return await Task.FromResult(false);
                Delete(invitationId);
            }

            else
            {
                var invitation = Query(x =>
                    x.Invitator.User.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null
                    && x.Id == invitationId && !x.IsAccepted).Select().FirstOrDefault();
                if (invitation == null) return await Task.FromResult(false);
                Delete(invitation.Id);
            }

            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> AcceptInvitation(long invitationId, Guid userToken)
        {
            var acceptor = _repositoryAsync.Query(user => user.Tokens.FirstOrDefault(token => token.TokenData == userToken) != null).Select().FirstOrDefault();
            if (acceptor == null) return await Task.FromResult(false);

            var invitation = Query(x => x.Id == invitationId).SelectAsync().Result.FirstOrDefault();
            if (invitation == null) return await Task.FromResult(false);

            invitation.AcceptorId = acceptor.UserId;
            invitation.IsAccepted = true;
            invitation.ObjectState = ObjectState.Modified;

            Update(invitation);
            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
