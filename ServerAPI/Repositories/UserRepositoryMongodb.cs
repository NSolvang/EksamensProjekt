using MongoDB.Driver;
using Core;
using Core.Factory;

namespace ServerAPI.Repositories;

public class UserRepositoryMongodb : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public UserRepositoryMongodb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var dbName = "comwellDB";
        var collectionName = "users";
        
        _userCollection = client.GetDatabase(dbName)
            .GetCollection<User>(collectionName);
    }

    public async Task<User[]> GetAll()
    {
        var noFilter = Builders<User>.Filter.Empty;
        var list = await _userCollection.Find(noFilter).ToListAsync();
        return list.ToArray();
    }

    public async Task AddUser(User user)
    {
        // Tildel nyt UserId baseret på højeste eksisterende ID
        var maxUser = await _userCollection.Find(Builders<User>.Filter.Empty)
            .SortByDescending(u => u.UserId)
            .FirstOrDefaultAsync();

        user.UserId = (maxUser?.UserId ?? 0) + 1;

        // Opret Studentplan hvis brugeren er en elev 
        if (user.Role == "Elev")
        {
            var plan = StudentplanFactory.CreateDefaultStudentplan();
            plan.StudentplanID = user.UserId;
            user.Studentplan = plan;
        }

        await _userCollection.InsertOneAsync(user);
    }


    public async Task DeleteById(int id)
    {
        var filter = Builders<User>.Filter.Eq("UserId", id);
        await _userCollection.DeleteOneAsync(filter);
    }

    public async Task UpdateUser(User user)
    {
        
        var filter = Builders<User>.Filter.Eq(a => a.UserId, user.UserId);
        await _userCollection.ReplaceOneAsync(filter, user);
    }

    public async Task<User> GetUserById(int id)
    {
        var filter = Builders<User>.Filter.Eq(a => a.UserId, id);
        var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
        return user;
    }
    
    public async Task<User?> Login(string username, string password)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserName, username) &
                     Builders<User>.Filter.Eq(u => u.Password, password);

        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    
}