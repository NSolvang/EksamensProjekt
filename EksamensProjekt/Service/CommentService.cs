using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

/// <summary>
/// Service til håndtering af kommentarer relateret til delmål (subgoals).
/// Kommunikerer med backend via HTTP og implementerer IComment interfacet.
/// </summary>
public class CommentService : IComment
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initialiserer en ny instans af <see cref="CommentService"/> med en HTTP-klient.
    /// </summary>
    /// <param name="client">HTTP-klient, der bruges til at sende forespørgsler til API'et.</param>
    public CommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Comment>> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalID)
    {
        var url = $"/api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals/{subgoalID}/comments";

        var result = await _client.GetFromJsonAsync<List<Comment>>(url);
        return result ?? new List<Comment>();
    }

    public async Task AddComment(int userId, int internshipId, int goalId, int subgoalId, Comment comment)
    {
        var url = $"/api/users/{userId}/studentplan/internships/{internshipId}/goals/{goalId}/subgoals/{subgoalId}/comments";

        var response = await _client.PostAsJsonAsync(url, comment);
        response.EnsureSuccessStatusCode();
    }
}