using Application.Services.JsonFileService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(User user);
        Task<User> LoginUser(string email);
    }

    class UserRepository : IUserRepository
    {
        private readonly IJsonFileService _jsonFileService;

        public UserRepository(IJsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        #region Login

        public async Task<User> LoginUser(string email)
        {
            var users = await _jsonFileService.ReadJsonFile<List<User>>("../../Data/users.json");
            return users.FirstOrDefault(x => x.Email.Equals(email));
        }

        #endregion

        #region Register
        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                var filePath = "../../Data/users.json";
                var users = await _jsonFileService.ReadJsonFile<List<User>>(filePath);
                users.Add(user);
                await _jsonFileService.WriteJsonFile(filePath, users);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
