using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Service til håndtering af lokationer, der kommunikerer med backend via HTTP.
/// Implementerer ILocation interfacet.
/// </summary>
public class LocationService : ILocation
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initialiserer en ny instans af <see cref="LocationService"/> med en <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="client">HTTP-klient, der bruges til at sende forespørgsler til API'et.</param>
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
}