using Core;
using EksamensProjekt.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
namespace ServerAPI.Controllers;

[ApiController]
[Route("api/users/{userId}/studentplan/goals/{goalId}/subgoals")]
public class SubgoalController : ControllerBase
{
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public SubgoalController(IUserRepository userRepository, ICommentRepository commentRepository)
        {
                _userRepository = userRepository;
                _commentRepository = commentRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSubgoal(int userId, int goalId, [FromBody] Subgoal newSubgoal)
        {
                try
                {
                        Console.WriteLine($"AddSubgoal kaldt med userId={userId}, goalId={goalId}, subgoal={newSubgoal.Name}");
        
                        // Her kalder du repository metoden (fx AddSubgoalToGoal)
                        var result = await _userRepository.AddSubgoalToGoal(userId, goalId, newSubgoal);
                        if (!result)
                        {
                                Console.WriteLine("Kunne ikke finde user eller goal.");
                                return NotFound();
                        }

                        return Ok("Subgoal added");
                }
                catch (Exception ex)
                {
                        Console.WriteLine("Exception i AddSubgoal: " + ex.ToString());
                        return StatusCode(500, "Intern serverfejl");
                }
        }
        
        [HttpDelete("{subgoalId}")]
        public async Task<IActionResult> DeleteSubgoal(int userId, int goalId, int subgoalId)
        {
            await _userRepository.DeleteSubgoalFromGoal(userId, goalId, subgoalId);
            return Ok("Subgoal deleted");
        }

        [HttpPut("{subgoalId}")]
        public async Task<IActionResult> UpdateSubgoal(
                int userId,
                int goalId,
                int subgoalId,
                [FromBody] Subgoal newSubgoal)
        {
                await _userRepository.UpdateSubgoalFromGoal(userId, goalId, subgoalId, newSubgoal);
                return Ok("Subgoal opdateret");
        }
        
        [HttpPost("{subgoalId}/comments")]
        public async Task<IActionResult> AddComment(int userId, int goalId, int subgoalId, [FromBody] Comment comment)
        {
                try
                {
                        Console.WriteLine($"Attempting to add comment for userId: {userId}, goalId: {goalId}, subgoalId: {subgoalId}");
        
                        comment.SubgoalID = subgoalId;
                        comment.CreatedAt = DateTime.UtcNow;

                        await _commentRepository.AddComment(userId, goalId, subgoalId, comment);
        
                        Console.WriteLine("Comment added successfully");
                        return Ok("Comment added");
                }
                catch (Exception ex)
                {
                        Console.WriteLine($"Error adding comment: {ex.ToString()}");
                        return StatusCode(500, ex.Message);
                }
        }

        [HttpGet("{subgoalId}/comments")]
        public async Task<IActionResult> GetCommentsBySubgoalId(int userId, int goalId, int subgoalId)
        {
                try
                {
                        var comments = await _commentRepository.GetCommentsBySubgoalId(userId, goalId, subgoalId);
                        return Ok(comments);
                }
                catch (Exception ex)
                {
                        Console.WriteLine("Error fetching comments: " + ex.Message);
                        return StatusCode(500, "Error fetching comments");
                }
        }







        

}