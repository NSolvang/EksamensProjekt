using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class UserService : IUser
{
    private string serverUrl = "http://localhost:5186";

    private HttpClient client;
    
    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User[]> GetAll()
    {
        return await client.GetFromJsonAsync<User[]>($"{serverUrl}/api/User");
    }

    public Task GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    
    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}/api/User/{id}");
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public async Task AddUser(User user)
    {
        await client.PutAsJsonAsync($"{serverUrl}/api/User", user);
    }
    
}