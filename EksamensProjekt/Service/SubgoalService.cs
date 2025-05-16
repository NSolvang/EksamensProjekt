using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class SubgoalService : ISubgoal
{
    private readonly HttpClient client;
    private readonly string serverUrl = "http://localhost:5186";

    public SubgoalService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<bool> AddSubgoalToGoal(int goalId, Subgoal newSubgoal)
    {
        try
        {
            var response = await client.PostAsJsonAsync($"{serverUrl}/api/Goal/{goalId}/subgoals", newSubgoal);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding subgoal: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSubgoal(int goalId, int subgoalId)
    {
        try
        {
            var response = await client.DeleteAsync($"{serverUrl}/api/Goal/{goalId}/subgoals/{subgoalId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting subgoal: {ex.Message}");
            return false;
        }
    }
}