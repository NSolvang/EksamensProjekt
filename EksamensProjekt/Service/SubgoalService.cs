using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class SubgoalService : ISubgoal
{
    private readonly HttpClient client;
    private readonly string serverUrl = "http://localhost:5094";

    public SubgoalService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<bool> AddSubgoalToGoal(int userId, int goalId, Subgoal newSubgoal)
    {
        try
        {
            var response = await client.PostAsJsonAsync(
                $"{serverUrl}/api/users/{userId}/studentplan/goals/{goalId}/subgoals", newSubgoal);
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
                $"{serverUrl}/api/users/{userId}/studentplan/goals/{goalId}/subgoals/{subgoalId}"
            );

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting subgoal: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> UpdateSubgoal(int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        try
        {
            var response = await client.PutAsJsonAsync($"{serverUrl}/api/Goal/{goalId}/subgoals/{subgoalId}", updatedSubgoal);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating subgoal: {ex.Message}");
            return false;
        }
    }
}