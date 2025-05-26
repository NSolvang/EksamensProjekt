using Core;

namespace EksamensProjekt.Service;

public interface ISubgoal
{
    Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal newSubgoal);
    Task<bool> DeleteSubgoal(int userId, int internshipId, int goalId, int subgoalId);
    Task<bool> UpdateSubgoal(int userId, int internshipId, int goalId, int subgoalId, Subgoal updatedSubgoal);
}
