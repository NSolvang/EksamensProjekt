using Core;

namespace EksamensProjekt.Service;

public class ServiceMock : IUser
{
    private List<User> users = new();
    public async Task<User[]> GetAll()
    {
        return users.ToArray();
    }

    public Task GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddUser(User user)
    {
        int max = 0;
        if (users.Count > 0)
            max = users.Select(user => user.UserId).Max();
        user.UserId = max + 1;
        users.Add(user);
    }

    public async Task DeleteById(int id)
    {
        users.RemoveAll(user => user.UserId == id);
    }
    
}