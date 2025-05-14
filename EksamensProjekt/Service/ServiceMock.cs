using Core;
using Blazored.LocalStorage;

namespace EksamensProjekt.Service;

public class ServiceMock : IUser
{
    private readonly ILocalStorageService _localStorage;
    
    private List<User> users = new();
    public async Task<User[]> GetAll()
    {
        var storedUsers = await _localStorage.GetItemAsync<User[]>("users");

        if (storedUsers != null)
        {
            users = storedUsers.ToList();
        }

        return users.ToArray();
    }
    
    public ServiceMock(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;

        if (users.Count == 0)
        {
            users.Add(new User
            {
                UserId = 1,
                UserName = "admin",
                Password = "admin123",
                Role = "Admin",
                Name = "Admin Test",
                Location = "Århus",
                ProfilePicture =
                    "https://static.independent.co.uk/2024/05/24/13/Gordon.jpg?width=1200&height=1200&fit=crop"
            });
        }
    }
    
    public Task<User> GetUserById(int id)
    {
        return Task.FromResult(users.FirstOrDefault(u => u.UserId == id));
    }
    
    public async Task AddUser(User user)
    {
        int max = 0;
        if (users.Count > 0)
            max = users.Select(user => user.UserId).Max();
        user.UserId = max + 1;
        users.Add(user);

        if (user.Role == "Elev")
        {
            user.Studentplan = new Studentplan()
            {
                StudentplanID = user.UserId,
                Name = $"{user.Name}s plan",
                Description = "Standard elevplan",
            };
        }
        await _localStorage.SetItemAsync("users", users);
    }

    public async Task DeleteById(int id)
    {
        users.RemoveAll(user => user.UserId == id);
        await _localStorage.SetItemAsync("users", users);
    }
    
}