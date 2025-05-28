using Core;
using MongoDB.Driver;

namespace ServerAPI.Repositories;

public class LocationRepositoryMongodb : ILocationRepository
{
    private readonly IMongoCollection<Location> _locationCollection;

    public LocationRepositoryMongodb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var database = client.GetDatabase("comwellDB");
        _locationCollection = database.GetCollection<Location>("locations");
    }

    public async Task<List<Location>> GetAllLocations()
    {
        return await _locationCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Location> GetLocationById(int id)
    {
        return await _locationCollection.Find(l => l._id == id).FirstOrDefaultAsync();
    }

    public async Task<Location> AddLocation(Location location)
    {
        // Generér et nyt _id
        var last = await _locationCollection
            .Find(_ => true)
            .SortByDescending(l => l._id)
            .FirstOrDefaultAsync();

        location._id = (last?._id ?? 0) + 1;
        
        await _locationCollection.InsertOneAsync(location);
        return location;
    }
}