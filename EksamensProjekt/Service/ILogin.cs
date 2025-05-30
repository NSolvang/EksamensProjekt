using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface til login-relaterede operationer såsom login, logout og hentning af den aktive bruger.
/// </summary>
public interface ILogin
{
    /// <summary>
    /// Forsøger at logge en bruger ind med brugernavn og adgangskode.
    /// </summary>
    /// <param name="userName">Brugerens brugernavn.</param>
    /// <param name="password">Brugerens adgangskode.</param>
    /// <returns>True hvis login lykkedes, ellers false.</returns>
    Task<bool> Login(string userName, string password);

    /// <summary>
    /// Henter den aktuelt loggede bruger, hvis nogen er logget ind.
    /// </summary>
    /// <returns>Den loggede bruger, eller null hvis ingen er logget ind.</returns>
    Task<User?> GetUserLoggedIn();

    /// <summary>
    /// Logger den aktuelle bruger ud.
    /// </summary>
    /// <returns>Task der repræsenterer asynkron logout-operation.</returns>
    Task LogOut();
}