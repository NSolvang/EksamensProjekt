using Core;
using Core.Filter;
namespace EksamensProjekt.Service;

public interface IUser
{
    Task<User[]> GetAll();
    
    Task<User> GetUserById(int id); 
    
    //Tilføjer ny User og tildeler dem et id
    Task AddUser(User user);
    
    //Fjerner bruger, hvor user.UserId == id
    Task DeleteById(int id);
    
    Task<User[]> GetFilteredUsers(UserFilter filter);

    //Opdaterer brugeren
    Task UpdateUser(User user);
    
}