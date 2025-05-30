using Core;
using System.Net.Http.Json;
using EksamensProjekt.Service;
using Blazored.LocalStorage;

namespace EksamensProjekt.Service;

public class LoginService : ILogin
{
    private readonly HttpClient client;
    private readonly ILocalStorageService localStorage;
    private readonly AuthStateService authStateService;  

    /// <summary>
    /// Initialiserer en ny instans af LoginService med nødvendige services.
    /// </summary>
    /// <param name="httpClient">HttpClient til API-kald.</param>
    /// <param name="localStorageService">Service til lokal lagring.</param>
    /// <param name="authState">Service til håndtering af autentificeringstilstand.</param>
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
            var loginRequest = new LoginDto
            {
                Username = UserName,
                Password = Password
            };

            var response = await client.PostAsJsonAsync($"api/User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    await localStorage.SetItemAsync("user", user);

                    // Notificer abonnenter om ændring i autentificeringstilstand
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

    public async Task LogOut()
    {
        try
        {
            await localStorage.RemoveItemAsync("user");

            var response = await client.PostAsync($"api/User/logout", null);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Logout failed on the server side.");
            }

            // Notificer abonnenter om ændring i autentificeringstilstand
            authStateService.NotifyAuthStateChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout error: {ex.Message}");
        }
    }
}
