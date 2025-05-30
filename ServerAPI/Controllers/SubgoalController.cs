using Core;
using EksamensProjekt.Service;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers;

/// <summary>
/// API-controller til håndtering af subgoals (delmål) under internships og goals for en specifik bruger.
/// Understøtter CRUD-operationer samt tilføjelse og visning af kommentarer til subgoals.
/// </summary>
[ApiController]
[Route("api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals")]
public class SubgoalController : ControllerBase
{
    private readonly IUserRepository _userRepository; // Repository til at håndtere brugerdata og subgoals
    private readonly ICommentRepository _commentRepository; // Repository til at håndtere kommentarer til subgoals

    /// <summary>
    /// Konstruktør som injicerer repositories til bruger- og kommentarhåndtering.
    /// </summary>
    /// <param name="userRepository">Repository til brugere og subgoals.</param>
    /// <param name="commentRepository">Repository til kommentarer.</param>
    public SubgoalController(IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    /// <summary>
    /// Tilføjer et subgoal til en bestemt goal i en praktikplan for en bruger.
    /// </summary>
    /// <param name="userId">ID for brugeren.</param>
    /// <param name="internshipId">ID for praktikforløbet.</param>
    /// <param name="goalId">ID for målet.</param>
    /// <param name="newSubgoal">Det nye subgoal der skal tilføjes.</param>
    /// <returns>HTTP 200 OK ved succes, 404 hvis data ikke findes, 500 ved fejl.</returns>
    [HttpPost]
    public async Task<IActionResult> AddSubgoal(int userId, int internshipId, int goalId, [FromBody] Subgoal newSubgoal)
    {
        try
        {
            Console.WriteLine($"AddSubgoal kaldt med userId={userId}, internshipId={internshipId}, goalId={goalId}, subgoal={newSubgoal.Name}");

            var result = await _userRepository.AddSubgoalToGoal(userId, internshipId, goalId, newSubgoal);
            if (!result)
            {
                Console.WriteLine("Kunne ikke finde user, internship eller goal.");
                return NotFound();
            }

            return Ok("Subgoal added");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception i AddSubgoal: " + ex);
            return StatusCode(500, "Intern serverfejl");
        }
    }

    /// <summary>
    /// Sletter et subgoal ud fra ID.
    /// </summary>
    /// <param name="userId">ID for brugeren.</param>
    /// <param name="internshipId">ID for praktikforløbet.</param>
    /// <param name="goalId">ID for målet.</param>
    /// <param name="subgoalId">ID for subgoalet.</param>
    /// <returns>HTTP 200 OK ved succes, 500 ved fejl.</returns>
    [HttpDelete("{subgoalId}")]
    public async Task<IActionResult> DeleteSubgoal(int userId, int internshipId, int goalId, int subgoalId)
    {
        try
        {
            await _userRepository.DeleteSubgoalFromGoal(userId, internshipId, goalId, subgoalId);
            return Ok("Subgoal deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Opdaterer et eksisterende subgoal.
    /// </summary>
    /// <param name="userId">ID for brugeren.</param>
    /// <param name="internshipId">ID for praktikforløbet.</param>
    /// <param name="goalId">ID for målet.</param>
    /// <param name="subgoalId">ID for subgoalet der skal opdateres.</param>
    /// <param name="updatedSubgoal">Det opdaterede subgoal-objekt.</param>
    /// <returns>HTTP 200 OK ved succes, 500 ved fejl.</returns>
    [HttpPut("{subgoalId}")]
    public async Task<IActionResult> UpdateSubgoal(
        int userId,
        int internshipId,
        int goalId,
        int subgoalId,
        [FromBody] Subgoal updatedSubgoal)
    {
        try
        {
            Console.WriteLine($"🔁 UpdateSubgoal kaldt med userId={userId}, internshipId={internshipId}, goalId={goalId}, subgoalId={subgoalId}");

            await _userRepository.UpdateSubgoalFromGoal(userId, internshipId, goalId, subgoalId, updatedSubgoal);
            return Ok("Subgoal opdateret");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af subgoal: {ex}");
            return StatusCode(500, $"Intern serverfejl: {ex.Message}");
        }
    }

    /// <summary>
    /// Tilføjer en kommentar til et subgoal.
    /// </summary>
    /// <param name="userId">ID for brugeren.</param>
    /// <param name="internshipId">ID for praktikforløbet.</param>
    /// <param name="goalId">ID for målet.</param>
    /// <param name="subgoalId">ID for subgoalet.</param>
    /// <param name="comment">Kommentaren der skal tilføjes.</param>
    /// <returns>HTTP 200 OK ved succes, 500 ved fejl.</returns>
    [HttpPost("{subgoalId}/comments")]
    public async Task<IActionResult> AddComment(int userId, int internshipId, int goalId, int subgoalId, [FromBody] Comment comment)
    {
        try
        {
            Console.WriteLine($"Attempting to add comment for userId={userId}, internshipId={internshipId}, goalId={goalId}, subgoalId={subgoalId}");

            comment.SubgoalID = subgoalId;
            comment.CreatedAt = DateTime.Now;

            await _commentRepository.AddComment(userId, internshipId, goalId, subgoalId, comment);

            return Ok("Comment added");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding comment: {ex}");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Henter alle kommentarer for et specifikt subgoal.
    /// </summary>
    /// <param name="userId">ID for brugeren.</param>
    /// <param name="internshipId">ID for praktikforløbet.</param>
    /// <param name="goalId">ID for målet.</param>
    /// <param name="subgoalId">ID for subgoalet.</param>
    /// <returns>En liste af kommentarer hvis fundet, ellers HTTP 500 ved fejl.</returns>
    [HttpGet("{subgoalId}/comments")]
    public async Task<IActionResult> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalId)
    {
        try
        {
            var comments = await _commentRepository.GetCommentsBySubgoalId(userId, internshipId, goalId, subgoalId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching comments: " + ex.Message);
            return StatusCode(500, "Error fetching comments");
        }
    }
}
