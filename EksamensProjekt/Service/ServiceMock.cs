using Core;
using Blazored.LocalStorage;
using Core.Factory;
using Core.Filter;

namespace EksamensProjekt.Service;

public class ServiceMock : IUser, ISubgoal
{
    private readonly ILocalStorageService _localStorage;
    
    private List<User> users = new();
    private List<Goal> goals = new();
    
    public ServiceMock(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;

        if (!users.Any(u => u.Role == "Admin"))
        {
            users.Add(new User
            {
                UserId = 1,
                UserName = "admin",
                Password = "admin123",
                Role = "Admin",
                Name = "Admin Test",
                Location = new Location { Name = "Aarhus" },
                ProfilePicture =
                    "https://static.independent.co.uk/2024/05/24/13/Gordon.jpg?width=1200&height=1200&fit=crop"
            });
        }
        
    }
    
    public async Task<User[]> GetAll()
    {
        var storedUsers = await _localStorage.GetItemAsync<User[]>("users");

        if (storedUsers != null)
        {
            users = storedUsers.ToList();
        }

        return users.ToArray();
    }

  
    
    private async Task LoadUsersAsync()
    {
        var storedUsers = await _localStorage.GetItemAsync<User[]>("users");
        if (storedUsers != null)
        {
            users = storedUsers.ToList();
        }
    }
    
    
    public async Task<User> GetUserById(int id)
    {
        await LoadUsersAsync(); // sørg for at opdatere users listen fra localStorage
        return users.FirstOrDefault(u => u.UserId == id);
    }

    
    public async Task AddUser(User user)
    {
        int max = 0;
        if (users.Count > 0)
            max = users.Select(user => user.UserId).Max();
        user.UserId = max + 1;
        users.Add(user);

        if (user.Role == "Elev")
        {
            var plan = StudentplanFactory.CreateDefaultStudentplan();
            plan.StudentplanID = user.UserId;
            
            user.Studentplan = plan;
        }

        await _localStorage.SetItemAsync("users", users);
    }


    public async Task DeleteById(int id)
    {
        users.RemoveAll(user => user.UserId == id);
        await _localStorage.SetItemAsync("users", users);
    }

    public Task<User[]> GetFilteredUsers(UserFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> AddSubgoalToGoal(int userId,int goalId, Subgoal newSubgoal)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteSubgoal(int userId,int goalId, int subgoalId)
    {
        throw new NotImplementedException();

    }
    
    public async Task<bool> UpdateSubgoal(int userId,int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        throw new NotImplementedException();

    }

    public Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal newSubgoal)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSubgoal(int userId, int internshipId, int goalId, int subgoalId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateSubgoal(int userId, int internshipId, int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        throw new NotImplementedException();
    }
}
