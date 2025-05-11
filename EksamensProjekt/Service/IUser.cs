using Core;
namespace EksamensProjekt.Service;

public interface IUser
{
    Task<User[]> GetAll();
    
    Task<User> GetUserById(int id); 
    
    Task<Student> GetStudentById(int id);
    
    Task AddUser(User user);
    
    Task DeleteById(int id);
    
    List<User> GetUsers();
}