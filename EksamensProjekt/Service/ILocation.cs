using Core;
namespace EksamensProjekt.Service;


public interface ILocation
{
    Task<List<Location>> GetAllLocations();
    Task<Location> GetLocationById(int id);
    Task<Location> AddLocation(Location location);
}
