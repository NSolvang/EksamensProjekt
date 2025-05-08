using Core;

namespace EksamensProjekt.Service;

public class ServiceMock : IUser
{
    private List<User> users = new();
    public async Task<User[]> GetAll()
    {
        return users.ToArray();
    }
    
    public ServiceMock()
    {
        if (users.Count == 0)
        {
            users.Add(new User
            {
                UserId = 1,
                UserName = "admin",
                Password = "admin123",
                Role = "Admin",
                Name = "Admin Test"
            });
            
            users.Add(new User
            {
                UserId = 2,
                UserName = "headchef",
                Password = "headchef123",
                Role = "Køkkenchef",
                Name = "KøkkenChef"
            });
            
            users.Add(new User
            {
                UserId = 3,
                UserName = "kok",
                Password = "kok12345",
                Role = "Faglært kok",
                Name = "Kok"
            });
            
            users.Add(new User
            {
                UserId = 4,
                UserName = "elev",
                Password = "elev1234",
                Role = "Elev",
                Name = "Elev"
            });
        }
    }

    public List<User> GetUsers()
    {
        return users;
    }
    
    public Task<User> GetUserById(int id)
    {
        return Task.FromResult(users.FirstOrDefault(u => u.UserId == id));
    }

    
    public async Task AddUser(User user)
    {
        int max = 0;
        if (users.Count > 0)
            max = users.Select(user => user.UserId).Max();
        user.UserId = max + 1;
        users.Add(user);

        user.Studentplan = new Studentplan
        {
            StudentplanID = user.UserId,
            UserID = user.UserId,
            Name = $"{user.Name}s plan",
            Description = "Standard elevplan",
            SubgoalID = 0
        };
    }

    public async Task DeleteById(int id)
    {
        users.RemoveAll(user => user.UserId == id);
    }
    
}