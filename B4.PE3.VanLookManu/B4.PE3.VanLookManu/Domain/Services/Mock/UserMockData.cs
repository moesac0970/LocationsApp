using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace B4.PE3.VanLookManu.Domain.Mock
{
    public class UserMockData : IUserDataService
    {
        private List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Manu"
            },
            new User
            {
                Id = 2,
                Name = "Zac"
            }
        };

        public async Task<User> GetUserById(int id)
        {
            var currentUser = users.FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(currentUser);
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await Task.FromResult(users);
        }

        public async Task<User> UpdateUser(User user)
        {
            var oldUser = users.FirstOrDefault(u => u.Id == user.Id);
            users.Remove(oldUser);
            users.Add(user);
            return await Task.FromResult(oldUser);
        }

        public async Task<User> DeleteUser(User user)
        {
            users.Remove(user);
            return await Task.FromResult(user);
        }

        public async Task<User> CreateUser(User user)
        {
            int id = users.Last().Id;
            users.Add(user);
            return await Task.FromResult(user);

        }
    }
}
