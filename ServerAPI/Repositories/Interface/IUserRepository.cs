namespace ServerAPI.Repositories;

using Core;
using Core.Filter;

/// <summary>
/// Interface for adgang til og håndtering af brugere i systemets datalag.
/// Implementeres af repository-klasser der kommunikerer med databasen.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Henter alle brugere.
    /// </summary>
    /// <returns>Array af alle brugere.</returns>
    Task<User[]> GetAll();

    /// <summary>
    /// Henter en specifik bruger baseret på ID.
    /// </summary>
    /// <param name="id">ID for den bruger der skal hentes.</param>
    /// <returns>Den fundne bruger eller null hvis ikke fundet.</returns>
    Task<User> GetUserById(int id);

    /// <summary>
    /// Tilføjer en ny bruger.
    /// </summary>
    /// <param name="user">Brugerobjektet der skal tilføjes.</param>
    Task AddUser(User user);

    /// <summary>
    /// Sletter en bruger baseret på ID.
    /// </summary>
    /// <param name="id">ID for den bruger der skal slettes.</param>
    Task DeleteById(int id);

    /// <summary>
    /// Opdaterer en eksisterende bruger.
    /// </summary>
    /// <param name="user">Brugerobjektet med opdaterede data.</param>
    Task UpdateUser(User user);

    /// <summary>
    /// Henter brugere baseret på et filter.
    /// </summary>
    /// <param name="filter">Filter til at specificere hvilke brugere der skal returneres.</param>
    /// <returns>Array af brugere som matcher filteret.</returns>
    Task<User[]> GetFilteredUsers(UserFilter filter);

    /// <summary>
    /// Tilføjer et nyt delmål til et specifikt mål for en bruger.
    /// </summary>
    /// <param name="userId">ID på brugeren.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet.</param>
    /// <param name="newSubgoal">Det nye delmål der skal tilføjes.</param>
    /// <returns>Bool der angiver om operationen lykkedes.</returns>
    Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal newSubgoal);

    /// <summary>
    /// Sletter et delmål fra et mål for en bruger.
    /// </summary>
    /// <param name="userId">ID på brugeren.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet.</param>
    /// <param name="subgoalId">ID på det delmål der skal slettes.</param>
    Task DeleteSubgoalFromGoal(int userId, int internshipId, int goalId, int subgoalId);

    /// <summary>
    /// Opdaterer et delmål til et mål for en bruger.
    /// </summary>
    /// <param name="userId">ID på brugeren.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet.</param>
    /// <param name="subgoldId">ID på det delmål der skal opdateres.</param>
    /// <param name="subgoal">Delmål med opdaterede data.</param>
    Task UpdateSubgoalFromGoal(int userId, int internshipId, int goalId, int subgoldId, Subgoal subgoal);

    /// <summary>
    /// Logger en bruger ind med brugernavn og adgangskode.
    /// </summary>
    /// <param name="username">Brugernavn.</param>
    /// <param name="password">Adgangskode.</param>
    /// <returns>Den fundne bruger eller null hvis login fejler.</returns>
    Task<User?> Login(string username, string password);
}
