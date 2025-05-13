using Core;
using System.Net.Http.Json;
using EksamensProjekt.Service;
using Blazored.LocalStorage;

namespace EksamensProjekt.Service;

public class LoginService : ILogin
{
    private readonly string serverUrl = "http://localhost:5186/";
    private readonly HttpClient client;
    private readonly ILocalStorageService localStorage;
    private readonly AuthStateService authStateService;  

    public LoginService(HttpClient httpClient, ILocalStorageService localStorageService, AuthStateService authState)
    {
        client = httpClient;
        localStorage = localStorageService;
        authStateService = authState;  
    }

    public async Task<User?> GetUserLoggedIn()
    {
        var res = await localStorage.GetItemAsync<User>("user");
        return res;
    }

    public async Task<bool> Login(string UserName, string Password)
    {
        try
        {
            // Create login request object
            var loginRequest = new LoginRequest
            {
                Username = UserName,
                Password = Password
            };

            // Send POST request with JSON body
            var response = await client.PostAsJsonAsync($"{serverUrl}api/User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    await localStorage.SetItemAsync("user", user);
                    
                    // Notificer alle subscribers om auth state change
                    authStateService.NotifyAuthStateChanged();
                    
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return false;
        }
    }

    public async Task AddUser(User user)
    {
        await client.PostAsJsonAsync($"{serverUrl}api/User", user);
    }

    public async Task LogOut()
    {
        try
        {
            await localStorage.RemoveItemAsync("user");
 
            var response = await client.PostAsync($"{serverUrl}api/User/logout", null);
 
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Logout failed on the server side.");
            }
            
            // Notificer alle subscribers om auth state change
            authStateService.NotifyAuthStateChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout error: {ex.Message}");
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}