using Core;
namespace EksamensProjekt.Service;

public interface IGoal
{
    Task<Goal[]> GetAllGoals();

}