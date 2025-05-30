using Core;

namespace ServerAPI.Repositories;

/// <summary>
/// Interface for adgang til og håndtering af standard studentplan i systemets datalag.
/// Implementeres af repository-klasser der kommunikerer med databasen.
/// </summary>
public interface IStudentplanRepository
{
    /// <summary>
    /// Henter standard studentplan.
    /// </summary>
    /// <returns>Standard studentplan.</returns>
    Task<Studentplan> GetDefaultPlan();
}