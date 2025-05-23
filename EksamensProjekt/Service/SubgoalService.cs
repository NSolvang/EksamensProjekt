using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class SubgoalService : ISubgoal
{
    private readonly HttpClient client;

    public SubgoalService(HttpClient client)    
    {
        this.client = client;
    }

    public async Task<bool> AddSubgoalToGoal(int userId, int goalId, Subgoal newSubgoal)
    {
        try
        {
            var response = await client.PostAsJsonAsync(
                $"/api/users/{userId}/studentplan/goals/{goalId}/subgoals", newSubgoal);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding subgoal: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> DeleteSubgoal(int userId, int goalId, int subgoalId)
    {
        try
        {
            var response = await client.DeleteAsync(
                $"/api/users/{userId}/studentplan/goals/{goalId}/subgoals/{subgoalId}"
            );

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting subgoal: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> UpdateSubgoal(int userId, int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        try
        {
            var url = $"/api/users/{userId}/studentplan/goals/{goalId}/subgoals/{subgoalId}";
            var response = await client.PutAsJsonAsync(url, updatedSubgoal);
        
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API fejl: {errorContent}");
                return false;
            }
        
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af subgoal: {ex.Message}");
            return false;
        }
    }
    

}