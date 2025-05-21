using Core;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Repositories;
namespace ServerAPI.Controllers;

[ApiController]
[Route("api/users/{userId}/studentplan/goals/{goalId}/subgoals")]
public class SubgoalController : ControllerBase
{
        private readonly IUserRepository _userRepository;

        public SubgoalController(IUserRepository userRepository)
        {
                _userRepository = userRepository;
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


     

}