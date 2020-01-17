using B4.PE3.VanLookManu.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace B4.PE3.VanLookManu.Domain.Services.Local
{
    class UserMockDataJson : IUserDataService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;

        public UserMockDataJson()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "users.json");

            //prevent self-referencing loops when saving Json
            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<User> GetUserById(int id)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(e => e.Id == id);
        }

        public async Task<User> UpdateUser(User user)
        {
            var users = await GetAllUsers();
            var userToUpdate = users.FirstOrDefault(e => e.Id == user.Id);
            users.Remove(userToUpdate);
            users.Add(userToUpdate);
            SaveUsersToJsonFile(users);
            return users.FirstOrDefault(e => e.Id == user.Id);
        }

        public async Task<User> CreateUser(User user)
        {
            User lastUser; int lastId = 0;

            var users = await GetUsersAsync();
            if (users.Count == 0)
            {
                lastId = 0;
            }
            else
            {
                lastUser = users.LastOrDefault();
                lastId = lastUser.Id + 1;
            }
            users.Add(new User { Id = lastId, Name = user.Name });
            SaveUsersToJsonFile(users);
            return await GetUserById(user.Id);
        }

        protected async Task<IList<User>> GetAllUsers()
        {
            try
            {
                string usersJson = File.ReadAllText(_filePath);
                var users = JsonConvert.DeserializeObject<IList<User>>(usersJson);
                return await Task.FromResult(users);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return empty collection on file not found, deserialization error, ...
            {
                return new List<User>();
            }
        }

        protected void SaveUsersToJsonFile(IEnumerable<User> users)
        {
            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            File.WriteAllText(_filePath, usersJson);
        }



        public async Task<IList<User>> GetUsersAsync()
        {
            try
            {
                string usersJson = File.ReadAllText(_filePath);
                var users = JsonConvert.DeserializeObject<IList<User>>(usersJson);
                return await Task.FromResult(users);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return empty collection on file not found, deserialization error, ...
            {
                return new List<User>();
            }
        }

        public async Task<User> DeleteUser(User user)
        {
            var users = await GetAllUsers();
            var oldUser = users.Where(u => u == user).FirstOrDefault();
            users.Remove(oldUser);
            SaveUsersToJsonFile(users);
            return await Task.FromResult(oldUser);
        }
    }
}
