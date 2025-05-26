using Core;
using EksamensProjekt.Service;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;

namespace ServerAPI.Controllers;

[ApiController]
[Route("api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals")]
public class SubgoalController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;

    public SubgoalController(IUserRepository userRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
    }

    // ✅ Tilføj delmål til bestemt internship/goal
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

    // ✅ Slet delmål
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

    // ✅ Opdater delmål
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

    // ✅ Tilføj kommentar til delmål
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

    // ✅ Hent kommentarer for subgoal
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
