using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface for adgang til og håndtering af kommentarer knyttet til delmål (subgoals) i systemets datalag.
/// Implementeres af repository-klasser der kommunikerer med databasen.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Henter alle kommentarer til et specifikt delmål.
    /// </summary>
    /// <param name="userId">ID for brugeren, som forespørger eller ejer kommentaren.</param>
    /// <param name="internshipId">ID for praktikforløbet kommentaren relaterer sig til.</param>
    /// <param name="goalId">ID for det overordnede mål, som delmålet tilhører.</param>
    /// <param name="subgoalID">ID for det specifikke delmål, som kommentarerne er tilknyttet.</param>
    /// <returns>En liste over kommentarer tilknyttet det angivne delmål.</returns>
    Task<List<Comment>> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalID);

    /// <summary>
    /// Tilføjer en kommentar til et bestemt delmål.
    /// </summary>
    /// <param name="userId">ID for brugeren, der skriver kommentaren.</param>
    /// <param name="internshipId">ID for praktikforløbet, som kommentaren vedrører.</param>
    /// <param name="goalId">ID for målet som delmålet tilhører.</param>
    /// <param name="subgoalId">ID for delmålet, som kommentaren er knyttet til.</param>
    /// <param name="comment">Selve kommentarobjektet der skal gemmes.</param>
    /// <returns>Task, der indikerer om operationen er gennemført.</returns>
    Task AddComment(int userId, int internshipId, int goalId, int subgoalId, Comment comment);
}