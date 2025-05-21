namespace ServerAPI.Repositories;
using Core;
public interface IUserRepository
{
    Task<User[]> GetAll();
    Task<User> GetUserById(int id);
    Task AddUser(User user);
    Task DeleteById(int id);
    Task UpdateUser(User user);
    
    Task<bool> AddSubgoalToGoal(int userId, int goalId, Subgoal newSubgoal);
    
    Task DeleteSubgoalFromGoal(int userId, int goalId, int subgoal);
    
    Task<User?> Login(string username, string password);

}