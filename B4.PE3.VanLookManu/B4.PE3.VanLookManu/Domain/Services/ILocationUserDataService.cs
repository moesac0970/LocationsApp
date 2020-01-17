using B4.PE3.VanLookManu.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace B4.PE3.VanLookManu.Domain.Services
{
    public interface ILocationUserDataService
    {
        Task<IQueryable<LocationUser>> GetLocationsAsync(int id);
        Task<LocationUser> CreateLocation(LocationUser location);
        Task<LocationUser> DeleteLocation(Guid id);
        Task<LocationUser> UpdateLocation(LocationUser location);
    }
}
