using System.Net.Http.Json;
using Core;
using Core.Filter;

namespace EksamensProjekt.Service;

public class UserService : IUser
{
    private HttpClient client;

    /// <summary>
    /// Initialiserer en ny instans af UserService med en HttpClient til API-kald.
    /// </summary>
    /// <param name="client">HttpClient til kommunikation med backend API.</param>
    public UserService(HttpClient client)
    {
        this.client = client;
    }


    public async Task<User[]> GetAll()
    {
        return await client.GetFromJsonAsync<User[]>($"/api/User");
    }


    public Task<User> GetUserById(int id)
    {
        return client.GetFromJsonAsync<User>($"/api/User/{id}");
    }
    

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"/api/User/{id}");
    }


    public async Task AddUser(User user)
    {
        var response = await client.PostAsJsonAsync($"/api/User", user);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Fejl ved oprettelse: {response.StatusCode}, {error}");
        }
    }


    public async Task UpdateUser(User user)
    {
        var response = await client.PutAsJsonAsync("/api/User", user);
        response.EnsureSuccessStatusCode(); 
    }

    /// <summary>
    /// Konverterer et UserFilter-objekt til en query-string, der kan vedhæftes til URL.
    /// </summary>
    /// <param name="filter">Filterparametre til brugerfiltrering.</param>
    /// <returns>Query-string repræsentation af filteret.</returns>
    private string ToQueryString(UserFilter filter)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(filter.LocationName))
            queryParams.Add($"locationName={Uri.EscapeDataString(filter.LocationName)}");

        if (!string.IsNullOrWhiteSpace(filter.Education))
            queryParams.Add($"education={Uri.EscapeDataString(filter.Education)}");

        if (!string.IsNullOrWhiteSpace(filter.Internshipyear))
            queryParams.Add($"internshipyear={Uri.EscapeDataString(filter.Internshipyear)}");

        if (!string.IsNullOrWhiteSpace(filter.Name))
            queryParams.Add($"name={Uri.EscapeDataString(filter.Name)}");
        
        if (filter.IsActive.HasValue)
            queryParams.Add($"isActive={filter.IsActive.Value.ToString().ToLower()}");
        
        return string.Join("&", queryParams);
    }


    public async Task<User[]> GetFilteredUsers(UserFilter filter)
    {
        var queryString = ToQueryString(filter);
        var url = $"/api/User/filtered";

        if (!string.IsNullOrEmpty(queryString))
            url += "?" + queryString;

        return await client.GetFromJsonAsync<User[]>(url);
    }
}
