namespace ServerAPI.Repositories;
using Core;
public interface IUserRepository
{
    Task<User[]> GetAll();
    Task<User> GetUserById(int id);
    Task AddUser(User user);
    Task DeleteById(int id);
    Task UpdateUser(User user);
}