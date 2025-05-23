using Core;

namespace EksamensProjekt.Service;

public interface ISubgoal
{
    Task<bool> AddSubgoalToGoal(int userId,int selectedGoalId, Subgoal newSubgoal);
    Task<bool> DeleteSubgoal(int userId, int goalId, int subgoalId);
    
    Task<bool> UpdateSubgoal(int userId,int goalId, int subgoalId, Subgoal updatedSubgoal);

}