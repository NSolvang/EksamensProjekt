using Core;

namespace ServerAPI.Repositories;

/// <summary>
/// Interface for adgang til og håndtering af lokationer i systemets datalag.
/// Implementeres af repository-klasser der kommunikerer med databasen.
/// </summary>
public interface ILocationRepository
{
    /// <summary>
    /// Henter alle lokationer.
    /// </summary>
    /// <returns>En liste over alle lokationer.</returns>
    Task<List<Location>> GetAllLocations();

    /// <summary>
    /// Henter en specifik lokation baseret på dens ID.
    /// </summary>
    /// <param name="id">ID for den lokation der skal hentes.</param>
    /// <returns>Den fundne lokation, eller null hvis ikke fundet.</returns>
    Task<Location> GetLocationById(int id);

    /// <summary>
    /// Tilføjer en ny lokation.
    /// </summary>
    /// <param name="location">Lokationen der skal tilføjes.</param>
    /// <returns>Den tilføjede lokation.</returns>
    Task<Location> AddLocation(Location location);
}