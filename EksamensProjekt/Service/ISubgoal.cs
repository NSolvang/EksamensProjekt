using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface til håndtering af delmål (Subgoal) i systemet.
/// Indeholder metoder til at tilføje, slette og opdatere delmål.
/// </summary>
public interface ISubgoal
{
    /// <summary>
    /// Tilføjer et nyt delmål til et specifikt mål.
    /// </summary>
    /// <param name="userId">Bruger-ID for ejeren af målet.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet, som delmålet tilføjes til.</param>
    /// <param name="newSubgoal">Det nye delmål, der skal tilføjes.</param>
    /// <returns>True hvis tilføjelsen lykkedes, ellers false.</returns>
    Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal newSubgoal);

    /// <summary>
    /// Sletter et eksisterende delmål.
    /// </summary>
    /// <param name="userId">Bruger-ID for ejeren af målet.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet som delmålet hører til.</param>
    /// <param name="subgoalId">ID på det delmål, der skal slettes.</param>
    /// <returns>True hvis sletningen lykkedes, ellers false.</returns>
    Task<bool> DeleteSubgoal(int userId, int internshipId, int goalId, int subgoalId);

    /// <summary>
    /// Opdaterer et eksisterende delmål.
    /// </summary>
    /// <param name="userId">Bruger-ID for ejeren af målet.</param>
    /// <param name="internshipId">ID på praktikforløbet.</param>
    /// <param name="goalId">ID på målet som delmålet hører til.</param>
    /// <param name="subgoalId">ID på det delmål, der skal opdateres.</param>
    /// <param name="updatedSubgoal">Det opdaterede delmål.</param>
    /// <returns>True hvis opdateringen lykkedes, ellers false.</returns>
    Task<bool> UpdateSubgoal(int userId, int internshipId, int goalId, int subgoalId, Subgoal updatedSubgoal);
}