using Blazored.LocalStorage;
using Core;
using EksamensProjekt.Service;

namespace EksamensProjekt.Service;

public class LoginServiceClientSide : ILogin
{
    private readonly ILocalStorageService localStorage;
    private readonly IUser userService;

    public LoginServiceClientSide(ILocalStorageService ls, IUser userService)
    {
        localStorage = ls;
        this.userService = userService; 
    }

    public async Task<User?> GetUserLoggedIn()
    {
        return await localStorage.GetItemAsync<User>("user");
    }

    public async Task<bool> Login(string userName, string password)
    {
        var user = userService.GetUsers().FirstOrDefault(u => u.UserName == userName && u.Password == password);

        if (user != null)
        {

            await localStorage.SetItemAsync("user", user);
            return true;
        }
        return false;
    }

    public async Task AddUser(User user)
    {
        await localStorage.SetItemAsync("user", user);
    }
    
    public async Task LogOut()
    {
        await localStorage.RemoveItemAsync("user");
    }
}