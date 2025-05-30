using Core;
namespace EksamensProjekt.Service;

/// <summary>
/// Interface for håndtering af lokationer.
/// Implementeres af services, der tilbyder CRUD-operationer på lokationer.
/// </summary>
public interface ILocation
{
    /// <summary>
    /// Henter alle lokationer.
    /// </summary>
    /// <returns>En liste af alle lokationer.</returns>
    Task<List<Location>> GetAllLocations();

    /// <summary>
    /// Tilføjer en ny lokation.
    /// </summary>
    /// <param name="location">Lokationen, der skal tilføjes.</param>
    /// <returns>Den tilføjede lokation, evt. med opdaterede felter (f.eks. ID).</returns>
    Task<Location> AddLocation(Location location);
}