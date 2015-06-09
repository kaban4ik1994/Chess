using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Helpers;
using Chess.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

namespace Chess.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IRepositoryAsync<UserRole> _userRoleRepositoryAsync;

        public UserService(IRepositoryAsync<User> repository, IUnitOfWorkAsync unitOfWorkAsync, IRepositoryAsync<UserRole> userRoleRepositoryAsync)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _userRoleRepositoryAsync = userRoleRepositoryAsync;
        }

        public Task<UserModel> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var passwordHash = PasswordHashHelper.GetHash(password);
            return Task.FromResult(Query(x =>
                x.Email.ToLower() == email.ToLower()
                && x.PasswordHash == passwordHash
                && x.Active).Select(user => new UserModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(role => role.Role.Name),
                    Token = user.Tokens.FirstOrDefault().TokenData
                }).FirstOrDefault());
        }

        public Task<bool> GetUserAccessByTokenQuery(Guid token, List<string> roles)
        {
            var result = _userRoleRepositoryAsync.Query(
                role => role.User.Tokens.Where(token1 => token1.TokenData == token).Count() != 0 && role.User.Active)
                .Select(role => role.Role.Name).ToList();
            return Task.FromResult(result.Count != 0 && result.Any(s => roles.Any(s1 => s == s1)));
        }

        public async Task<long> AddUser(User user)
        {
            Insert(user);
            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(user.UserId);
        }
    }
}
