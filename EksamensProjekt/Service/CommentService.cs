using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class CommentService : IComment
{
    private readonly HttpClient _client;

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