using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Entities.Models;
using Chess.Models;
using Service.Pattern;

namespace Chess.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<UserModel> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<bool> GetUserAccessByTokenQuery(Guid token, List<string> roles);
        Task<long> AddUser(User user);
    }
}
