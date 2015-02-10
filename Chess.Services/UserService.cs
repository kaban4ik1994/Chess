using Chess.Entities.Models;
using Chess.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace Chess.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IRepositoryAsync<User> _repository;

        public UserService(IRepositoryAsync<User> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}
