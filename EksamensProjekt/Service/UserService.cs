using System.Net.Http.Json;
using Core;
using Core.Filter;

namespace EksamensProjekt.Service;

public class UserService : IUser
{
    private readonly string serverUrl = "http://localhost:5094";

    private HttpClient client;
    
    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User[]> GetAll()
    {
        return await client.GetFromJsonAsync<User[]>($"{serverUrl}/api/User");
    }

    public Task<User> GetUserById(int id)
    {
        return  client.GetFromJsonAsync<User>($"{serverUrl}/api/User/{id}");
    }
    
    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}/api/User/{id}");
    }

    
    public async Task AddUser(User user)
    {
        var response = await client.PostAsJsonAsync($"{serverUrl}/api/User", user);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Fejl ved oprettelse: {response.StatusCode}, {error}");
        }
    }
    
    public async Task UpdateUser(User user)
    {
        await client.PutAsJsonAsync($"{serverUrl}/api/User", user);
    }
    
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

        return string.Join("&", queryParams);
    }

    public async Task<User[]> GetFilteredUsers(UserFilter filter)
    {
        var queryString = ToQueryString(filter);
        var url = $"{serverUrl}/api/User/filtered";

        if (!string.IsNullOrEmpty(queryString))
            url += "?" + queryString;

        return await client.GetFromJsonAsync<User[]>(url);
    }

    
}