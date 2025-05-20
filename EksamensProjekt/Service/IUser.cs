using Core;
namespace EksamensProjekt.Service;

public interface IUser
{
    Task<User[]> GetAll();
    
    Task<User> GetUserById(int id); 
    
    //Tilføjer ny User og tildeler dem et id
    Task AddUser(User user);
    
    //Fjerner bruger, hvor user.UserId == id
    Task DeleteById(int id);
    //Opdaterer brugeren
    Task UpdateUser(User user);

}