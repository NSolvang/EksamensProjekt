using MongoDB.Driver;
using Core;
using Core.Filter;
using Core.Factory;

namespace ServerAPI.Repositories;

public class UserRepositoryMongodb : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;
    
    private readonly IStudentplanRepository _studentplanRepository;


    public UserRepositoryMongodb(IStudentplanRepository studentplanRepository)
    {
        _studentplanRepository = studentplanRepository;

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

        if (user.Role == "Elev")
        {
            var template = await _studentplanRepository.GetDefaultPlan();
            template.StudentplanID = user.UserId; 
            user.Studentplan = template;
        }

        await _userCollection.InsertOneAsync(user);
    }


    public async Task DeleteById(int id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserId, id);
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
    
    public async Task<bool> AddSubgoalToGoal(int userId, int goalId, Subgoal subgoal)
    {
        Console.WriteLine($"Forsøger at finde user med id {userId}");
        var user = await _userCollection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            Console.WriteLine("User ikke fundet");
            return false;
        }

        Console.WriteLine("User fundet, leder efter goal med id " + goalId);
        var goal = user.Studentplan?.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
        {
            Console.WriteLine("Goal ikke fundet");
            return false;
        }

        if (goal.Subgoals == null)
        {
            goal.Subgoals = new List<Subgoal>();
            Console.WriteLine("Oprettede ny subgoal liste");
        }

        Console.WriteLine("Tilføjer subgoal til goal");
        goal.Subgoals.Add(subgoal);

        // Gem opdateret user
        await _userCollection.ReplaceOneAsync(u => u.UserId == userId, user);

        Console.WriteLine("Subgoal tilføjet og gemt");
        return true;
    }

    public async Task DeleteSubgoalFromGoal(int userId, int goalId, int subgoalId)
    {
        var user = await _userCollection.Find(u => u.UserId == userId).FirstOrDefaultAsync();

        var goal = user.Studentplan.Goal.FirstOrDefault(g => g.GoalId == goalId);
        goal?.Subgoals.RemoveAll(sg => sg.SubgoalID == subgoalId);

        await _userCollection.ReplaceOneAsync(u => u.UserId == userId, user);
    }

    public async Task UpdateSubgoalFromGoal(int userId, int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        var user = await _userCollection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
        if (user == null)
            throw new Exception("User not found");

        var goal = user.Studentplan.Goal.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
            throw new Exception("Goal not found");

        var subgoalIndex = goal.Subgoals.FindIndex(s => s.SubgoalID == subgoalId);
        if (subgoalIndex == -1)
            throw new Exception("Subgoal not found");

        updatedSubgoal.SubgoalID = subgoalId;

        // Opdater subgoalen i listen
        goal.Subgoals[subgoalIndex] = updatedSubgoal;

        // Gem hele user-dokumentet tilbage i databasen
        var result = await _userCollection.ReplaceOneAsync(u => u.UserId == userId, user);

        if (!result.IsAcknowledged || result.ModifiedCount == 0)
            throw new Exception("Failed to update user");
    }
    
    public async Task<User[]> GetFilteredUsers(UserFilter filter)
    {
        var builder = Builders<User>.Filter;
        var mongoFilter = builder.Empty;

        if (!string.IsNullOrWhiteSpace(filter.LocationName))
            mongoFilter &= builder.Eq(u => u.Location.Name, filter.LocationName);

        if (!string.IsNullOrWhiteSpace(filter.Education))
            mongoFilter &= builder.Eq(u => u.Education, filter.Education);

        if (!string.IsNullOrWhiteSpace(filter.Internshipyear))
            mongoFilter &= builder.Eq(u => u.Internshipyear, filter.Internshipyear);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            mongoFilter &= builder.Regex(u => u.Name, new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"));

        var results = await _userCollection.Find(mongoFilter).ToListAsync();
        return results.ToArray();
    }


    
}