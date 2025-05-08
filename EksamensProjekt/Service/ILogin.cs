using Core;
namespace EksamensProjekt.Service;

public interface ILogin
{
    Task<bool> Login(string userName, string password);
    Task<User?> GetUserLoggedIn();
    Task LogOut();
}