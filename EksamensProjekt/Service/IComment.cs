using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface for kommentar-funktionalitet knyttet til specifikke delmål (subgoals).
/// Implementeres af services der håndterer hentning og tilføjelse af kommentarer.
/// </summary>
public interface IComment
{
    /// <summary>
    /// Henter alle kommentarer til et givent delmål (subgoal).
    /// </summary>
    /// <param name="userId">ID på brugeren, som forespørger eller ejer kommentaren.</param>
    /// <param name="internshipId">ID på praktikforløbet kommentaren relaterer sig til.</param>
    /// <param name="goalId">ID på det overordnede mål, som delmålet tilhører.</param>
    /// <param name="subgoalID">ID på det specifikke delmål, som kommentarerne hører til.</param>
    /// <returns>En liste af kommentarer knyttet til det givne delmål.</returns>
    Task<List<Comment>> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalID);

    /// <summary>
    /// Tilføjer en ny kommentar til et specifikt delmål.
    /// </summary>
    /// <param name="userId">ID på brugeren der skriver kommentaren.</param>
    /// <param name="internshipId">ID på praktikforløbet kommentaren vedrører.</param>
    /// <param name="goalId">ID på målet som delmålet tilhører.</param>
    /// <param name="subgoalId">ID på det delmål kommentaren er knyttet til.</param>
    /// <param name="comment">Selve kommentaren der skal gemmes.</param>
    /// <returns>Task der signalerer afslutning af operationen.</returns>
    Task AddComment(int userId, int internshipId, int goalId, int subgoalId, Comment comment);
}