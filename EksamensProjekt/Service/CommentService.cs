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

    public async Task<List<Comment>> GetCommentsBySubgoalId(int userId, int goalId, int subgoalID)
    {
        var result = await _client.GetFromJsonAsync<List<Comment>>(
            $"/api/users/{userId}/studentplan/goals/{goalId}/subgoals/{subgoalID}/comments");
        return result ?? new List<Comment>();
    }

    public async Task AddComment(int userId, int goalId, int subgoalId, Comment comment)
    {
        var response = await _client.PostAsJsonAsync(
            $"/api/users/{userId}/studentplan/goals/{goalId}/subgoals/{subgoalId}/comments", 
            comment);
        response.EnsureSuccessStatusCode();
    }

}
