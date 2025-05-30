using Core;
using Core.Filter;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface til CRUD-operationer og filtrering af brugerdata.
/// </summary>
public interface IUser
{
    /// <summary>
    /// Henter alle brugere.
    /// </summary>
    /// <returns>Array af alle brugere.</returns>
    Task<User[]> GetAll();
    
    /// <summary>
    /// Henter en bruger ud fra ID.
    /// </summary>
    /// <param name="id">Brugerens ID.</param>
    /// <returns>Den fundne bruger.</returns>
    Task<User> GetUserById(int id);
    
    /// <summary>
    /// Tilføjer en ny bruger og tildeler et ID.
    /// </summary>
    /// <param name="user">Brugerobjektet, der skal tilføjes.</param>
    Task AddUser(User user);
    
    /// <summary>
    /// Sletter en bruger ud fra ID.
    /// </summary>
    /// <param name="id">Brugerens ID.</param>
    Task DeleteById(int id);
    
    /// <summary>
    /// Henter brugere baseret på filterkriterier.
    /// </summary>
    /// <param name="filter">Filter til søgning.</param>
    /// <returns>Array af filtrerede brugere.</returns>
    Task<User[]> GetFilteredUsers(UserFilter filter);

    /// <summary>
    /// Opdaterer en eksisterende bruger.
    /// </summary>
    /// <param name="user">Brugerobjektet med opdaterede data.</param>
    Task UpdateUser(User user);
}