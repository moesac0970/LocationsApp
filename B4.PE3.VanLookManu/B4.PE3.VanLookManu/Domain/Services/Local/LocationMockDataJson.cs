using B4.PE3.VanLookManu.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.Domain.Services.Local
{
    public class LocationMockDataJson : ILocationUserDataService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;
        public LocationMockDataJson()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "locations.json");
            //prevent self-referencing loops when saving Json
            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<LocationUser> CreateLocation(LocationUser location)
        {
            var locations = await GetAllLocations();
            locations.Add(location);
            SaveLocationsToJsonFile(locations);
            return await Task.FromResult(location);
        }

        public async Task<LocationUser> DeleteLocation(Guid id)
        {
            var locations = await GetAllLocations();
            var LocationToRemove = locations.FirstOrDefault(loc => loc.Id == id);
            locations.Remove(LocationToRemove);
            SaveLocationsToJsonFile(locations);
            return await Task.FromResult(LocationToRemove);
        }

        public async Task<IQueryable<LocationUser>> GetLocationsAsync(int id)
        {
            var locations = await GetAllLocations();
            var locationsUser = locations.Where(loc => loc.Owner == id).AsQueryable();
            return await Task.FromResult(locationsUser);
        }

        public async Task<LocationUser> UpdateLocation(LocationUser location)
        {
            var locations = await GetAllLocations();
            var locationToUpdate = locations.FirstOrDefault(loc => loc.Id == location.Id);
            locations.Remove(locationToUpdate);
            locationToUpdate = location;
            locations.Add(locationToUpdate);
            SaveLocationsToJsonFile(locations);
            return await Task.FromResult(locationToUpdate);

        }
        protected void SaveLocationsToJsonFile(IEnumerable<LocationUser> locations)
        {
            string locationsJson = JsonConvert.SerializeObject(locations, _serializerSettings);
            File.WriteAllText(_filePath, locationsJson);
        }
        protected async Task<IList<LocationUser>> GetAllLocations()
        {
            try
            {
                string locationJson = File.ReadAllText(_filePath);
                IList<LocationUser> locations = Device.RuntimePlatform == Device.UWP 
                                        ? JsonConvert.DeserializeObject<IList<LocationUser>>(locationJson) // uwp serialises correct object
                                        : JsonConvert.DeserializeObject<IList<LocationUser>>(locationJson, new ColorConverter());

                Console.WriteLine("he");
                return await Task.FromResult(locations);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return empty collection on file not found, deserialization error, ...
            {
                return new List<LocationUser>();
            }
        }

    }
}
