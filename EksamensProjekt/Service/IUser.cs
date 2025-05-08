using Core;
namespace EksamensProjekt.Service;

public interface IUser
{
    Task<User[]> GetAll();
    
    Task GetUserById(int id);
    
    Task AddUser(User user);
    
    Task DeleteById(int id);
    
    List<User> GetUsers();
}