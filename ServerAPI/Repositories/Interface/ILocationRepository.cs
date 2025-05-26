using Core;
namespace ServerAPI.Repositories;

public interface ILocationRepository
{
    Task<List<Location>> GetAllLocations();
    Task<Location> GetLocationById(int id);
    Task<Location> AddLocation(Location location);
    
}