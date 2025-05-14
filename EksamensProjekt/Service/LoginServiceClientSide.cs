using Blazored.LocalStorage;
using Core;
using System.Linq;
using System.Threading.Tasks;

namespace EksamensProjekt.Service
{
    public class LoginServiceClientSide : ILogin
    {
        private readonly ILocalStorageService localStorage;
        private readonly IUser userService;
        private readonly AuthStateService authStateService;

        public LoginServiceClientSide(ILocalStorageService ls, IUser userService, AuthStateService authStateService)
        {
            localStorage = ls;
            this.userService = userService;
            this.authStateService = authStateService;
        }

        public async Task<User?> GetUserLoggedIn()
        {
            return await localStorage.GetItemAsync<User>("user");
        }

        public async Task<bool> Login(string userName, string password)
        {
            var users = await userService.GetAll(); // venter på at få User[]
            var user = users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user != null)
            {
                await localStorage.SetItemAsync("user", user);
                await localStorage.SetItemAsync("isLoggedIn", true);
                
                // Notificer andre komponenter om ændringen i auth state
                authStateService.NotifyAuthStateChanged();
                
                return true;
            }
            return false;
        }

        public async Task AddUser(User user)
        {
            await localStorage.SetItemAsync("user", user);
            
            // Notificer om ændring hvis nødvendigt
            authStateService.NotifyAuthStateChanged();
        }
        
        public async Task LogOut()
        {
            await localStorage.RemoveItemAsync("user");
            await localStorage.RemoveItemAsync("isLoggedIn");
            
            // Notificer om ændring
            authStateService.NotifyAuthStateChanged();
        }
    }
}