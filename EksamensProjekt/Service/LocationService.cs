using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class LocationService : ILocation
{
    private readonly HttpClient _client;

    public LocationService(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Location>> GetAllLocations()
    {
        return await _client.GetFromJsonAsync<List<Location>>("api/locations") ?? new List<Location>();
    }

    public async Task<Location> AddLocation(Location location)
    {
        var response = await _client.PostAsJsonAsync("api/locations", location);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Location>();
    }

    public async Task <Location?> GetLocationById(int id)
    {
        return await _client.GetFromJsonAsync<Location>($"api/locations/{id}");
    }
}
