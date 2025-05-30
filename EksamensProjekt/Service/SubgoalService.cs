using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Service til håndtering af delmål (Subgoal) via HTTP-kald til API.
/// Implementerer ISubgoal interface for at tilføje, slette og opdatere delmål.
/// </summary>
public class SubgoalService : ISubgoal
{
    private readonly HttpClient client;

    /// <summary>
    /// Initialiserer en ny instans af SubgoalService med en HttpClient.
    /// </summary>
    /// <param name="client">HttpClient til at udføre HTTP-forespørgsler.</param>
    public SubgoalService(HttpClient client)
    {
        this.client = client;
    }

    /// <inheritdoc />
    public async Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal newSubgoal)
    {
        try
        {
            var url = $"/api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals";
            var response = await client.PostAsJsonAsync(url, newSubgoal);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af subgoal: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteSubgoal(int userId, int internshipId, int goalId, int subgoalId)
    {
        try
        {
            var url = $"/api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals/{subgoalId}";
            var response = await client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved sletning af subgoal: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdateSubgoal(int userId, int internshipId, int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        try
        {
            var url = $"/api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals/{subgoalId}";
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
