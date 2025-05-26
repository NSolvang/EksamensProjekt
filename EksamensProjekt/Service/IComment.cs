using Core;

namespace EksamensProjekt.Service;

public interface IComment
{
    Task<List<Comment>> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalID);
    Task AddComment(int userId, int internshipId, int goalId, int subgoalId, Comment comment);
}
