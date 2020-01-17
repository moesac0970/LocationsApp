using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace B4.PE3.VanLookManu.Domain.Mock
{
    /// <summary>
    /// locatioList mock data class
    /// </summary>
    public class LocationMockData : ILocationUserDataService
    {
        private static List<LocationUser> _locationList = new List<LocationUser>
        {
            new LocationUser
            {
                Owner = 1,
                Created = DateTime.UtcNow,
                Location = new Location(47.645160, -122.1306032),
                Name = "Usa",
                Id = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                Color =  Color.Yellow
            },
            new LocationUser
            {
                Owner = 1,
                Created = DateTime.UtcNow,
                Location = new Location(48.6656546, 100),
                Name = "Siberia",
                Id = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                Color =  Color.Red
            },
            new LocationUser
            {
                Owner = 2,
                Created = DateTime.UtcNow,
                Location = new Location(47.645160, -122.1306032),
                Name = "Russia",
                Id = Guid.Parse("22222222-0000-0000-0000-000000000001"),
                Color =  Color.Blue
            },
            new LocationUser
            {
                Owner = 1,
                Created = DateTime.UtcNow,
                Location = new Location(48.6656546, 100),
                Name = "Vietnam",
                Id = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                Color =  Color.LightGray
            }

        };

        public async Task<IQueryable<LocationUser>> GetLocationsAsync(int id)
        {
            var locationListUser = _locationList.AsQueryable().Where(loc => loc.Owner == id);
            return await Task.FromResult(locationListUser);
        }

        public async Task<LocationUser> DeleteLocation(Guid id)
        {
            var oldLocation = _locationList.FirstOrDefault(loc => loc.Id == id);
            _locationList.Remove(oldLocation);
            return await Task.FromResult(oldLocation);
        }

        public async Task<LocationUser> UpdateLocation(LocationUser _location)
        {
            var location = _locationList.FirstOrDefault(loc => loc.Id == _location.Id);
            _locationList.Remove(location);
            _locationList.Add(_location);
            return await Task.FromResult(_location);
        }

        public Task<LocationUser> CreateLocation(LocationUser location)
        {
            _locationList.Add(location);
            return Task.FromResult(location);
        }
    }
}
