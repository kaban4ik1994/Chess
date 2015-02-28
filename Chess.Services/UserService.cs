using System;
using System.Collections.Generic;
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
        
        public UserService(IRepositoryAsync<User> repository, IUnitOfWorkAsync unitOfWorkAsync)
            : base(repository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
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
            var result = Task.FromResult(Query(user1 => user1.Tokens.Any(token1 => token1.TokenData == token) && user1.Active)
              .Include(user1 => user1.UserRoles)
              .Include(user1 => user1.UserRoles.Select(role => role.Role))
              .Select()
              .Any(user1 => user1.UserRoles.Any(role => roles.Any(s => s == role.Role.Name))));
            return result;
        }

        public async Task<long> AddUser(User user)
        {
            Insert(user);
            await _unitOfWorkAsync.SaveChangesAsync();
            return await Task.FromResult(user.UserId);
        }
    }
}
