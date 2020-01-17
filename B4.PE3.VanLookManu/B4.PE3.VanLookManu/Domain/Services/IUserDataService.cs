using B4.PE3.VanLookManu.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B4.PE3.VanLookManu.Domain.Services
{
    public interface IUserDataService
    {
        Task<User> GetUserById(int id);
        Task<IList<User>> GetUsersAsync();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);

    }
}
