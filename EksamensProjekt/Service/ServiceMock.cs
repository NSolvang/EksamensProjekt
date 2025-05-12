using Core;
using Blazored.LocalStorage;

namespace EksamensProjekt.Service;

public class ServiceMock : IUser
{
    private readonly ILocalStorageService _localStorage;
    
    private List<User> users = new();
    public async Task<User[]> GetAll()
    {
        return users.ToArray();
    }
    
    public ServiceMock(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;

        if (users.Count == 0)
        {
            users.Add(new User
            {
                UserId = 1,
                UserName = "admin",
                Password = "admin123",
                Role = "Admin",
                Name = "Admin Test",
                ProfilePicture = "https://static.independent.co.uk/2024/05/24/13/Gordon.jpg?width=1200&height=1200&fit=crop"
            });
            
            users.Add(new User
            {
                UserId = 2,
                UserName = "headchef",
                Password = "headchef123",
                Role = "Køkkenchef",
                Name = "KøkkenChef",
                ProfilePicture = "https://static.independent.co.uk/2024/05/24/13/Gordon.jpg?width=1200&height=1200&fit=crop"
            });
            
            users.Add(new User
            {
                UserId = 3,
                UserName = "kok",
                Password = "kok12345",
                Role = "Faglært kok",
                Name = "Kok",
                ProfilePicture = "https://static.independent.co.uk/2024/05/24/13/Gordon.jpg?width=1200&height=1200&fit=crop"
            });
            
            users.Add(new Student
            {
                UserId = 4,
                UserName = "elev",
                Password = "elev1234",
                Role = "Elev",
                Name = "Elev",
                ProfilePicture = "https://media.istockphoto.com/id/1763926700/photo/portrait-of-smiling-smart-school-boy-wearing-braces-on-teeth-looking-at-camera-education.jpg?s=612x612&w=0&k=20&c=kDQg5b1no9fvjtsmdme9aB-oRd3xmXroT4577FL2pb4=",
                Studentplan = new Studentplan()
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
    
    public Task<Student?> GetStudentById(int id)
    {
        var student = users
            .OfType<Student>()        // Filtrér kun de objekter, der faktisk er af typen Student
            .FirstOrDefault(s => s.UserId == id);

        return Task.FromResult(student);
    }

    
    public async Task AddUser(User user)
    {
        int max = 0;
        if (users.Count > 0)
            max = users.Select(user => user.UserId).Max();
        user.UserId = max + 1;
        users.Add(user);

        if (user is Student student)
        {
            student.Studentplan = new Studentplan
            {
                StudentplanID = user.UserId,
                UserID = user.UserId,
                Name = $"{user.Name}s plan",
                Description = "Standard elevplan",
                SubgoalID = 0
            };
        }
      
    }

    public async Task DeleteById(int id)
    {
        users.RemoveAll(user => user.UserId == id);
    }
    
}