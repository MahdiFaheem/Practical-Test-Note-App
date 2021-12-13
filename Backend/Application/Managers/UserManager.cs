using Application.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Managers
{
    public interface IUserManager
    {
        Task<bool> RegisterUser(User user);
        Task<User> LoginUser(string email);
    }
    class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;


        public UserManager(IUserRepository userRepostory)
        {
            _userRepository = userRepostory;
        }

        public Task<bool> RegisterUser(User user)
        {
            return _userRepository.RegisterUser(user);
        }

        public Task<User> LoginUser(string email)
        {
            return _userRepository.LoginUser(email);
        }
    }
}
