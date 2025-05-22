using Core;

namespace EksamensProjekt.Service;

public interface IComment
{
    Task<List<Comment>> GetCommentsBySubgoalId(int userId, int goalId, int subgoalID);
    Task AddComment(int userId, int goalId, int subgoalId, Comment comment);
}