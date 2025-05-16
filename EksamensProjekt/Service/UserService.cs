using System.Net.Http.Json;
using Core;

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
        await client.PostAsJsonAsync($"{serverUrl}/api/User", user);
    }

    public async Task UpdateUser(User user)
    {
        await client.PutAsJsonAsync($"{serverUrl}/api/User", user);
    }
    
}