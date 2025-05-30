using MongoDB.Driver;
using Core;
using Core.Filter;
using Core.Factory;

namespace ServerAPI.Repositories;

/// <summary>
/// Repository der håndterer CRUD-operationer og subgoal-håndtering for brugere i MongoDB.
/// </summary>
public class UserRepositoryMongodb : IUserRepository
{
    /// <summary>
    /// MongoDB collection til brugere.
    /// </summary>
    private readonly IMongoCollection<User> _userCollection;
    
    /// <summary>
    /// Repository til håndtering af standard elevplan.
    /// </summary>
    private readonly IStudentplanRepository _studentplanRepository;

    /// <summary>
    /// Initialiserer UserRepositoryMongodb med reference til StudentplanRepository og forbinder til MongoDB.
    /// </summary>
    /// <param name="studentplanRepository">Repository til håndtering af studentplan.</param>
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
        // Tildel nyt _id baseret på højeste eksisterende ID
        var maxUser = await _userCollection.Find(Builders<User>.Filter.Empty)
            .SortByDescending(u => u._id)
            .FirstOrDefaultAsync();

        user._id = (maxUser?._id ?? 0) + 1;

        if (user.Role == "Elev")
        {
            var template = await _studentplanRepository.GetDefaultPlan();
            template._id = user._id;
            user.Studentplan = template;
        }

        await _userCollection.InsertOneAsync(user);
    }

    public async Task DeleteById(int id)
    {
        var filter = Builders<User>.Filter.Eq(u => u._id, id);
        await _userCollection.DeleteOneAsync(filter);
    }

    public async Task UpdateUser(User user)
    {
        try
        {
            var filter = Builders<User>.Filter.Eq(u => u._id, user._id);
            await _userCollection.ReplaceOneAsync(filter, user);
            Console.WriteLine("Bruger blev updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl i UpdateUser: {ex.Message}");
            throw;
        }
    }

    public async Task<User> GetUserById(int id)
    {
        var filter = Builders<User>.Filter.Eq(a => a._id, id);
        var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User?> Login(string username, string password)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserName, username) &
                     Builders<User>.Filter.Eq(u => u.Password, password);

        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> AddSubgoalToGoal(int userId, int internshipId, int goalId, Subgoal subgoal)
    {
        var user = await _userCollection.Find(u => u._id == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            Console.WriteLine("User ikke fundet");
            return false;
        }

        if (user.Studentplan?.Internship == null)
        {
            Console.WriteLine("Ingen internships fundet på user.Studentplan");
            return false;
        }

        var internship = user.Studentplan.Internship.FirstOrDefault(i => i._id == internshipId);
        if (internship == null)
        {
            Console.WriteLine("Internship ikke fundet");
            return false;
        }

        var goal = internship.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
        {
            Console.WriteLine("Goal ikke fundet i det valgte internship");
            return false;
        }

        if (goal.Subgoals == null)
        {
            goal.Subgoals = new List<Subgoal>();
            Console.WriteLine("Oprettede ny subgoal-liste");
        }

        int nextId = GetNextUniqueSubgoalId(user);
        subgoal.SubgoalID = nextId;
        goal.Subgoals.Add(subgoal);

        await _userCollection.ReplaceOneAsync(u => u._id == userId, user);
        Console.WriteLine($"Subgoal tilføjet med unikt ID: {nextId}");

        return true;
    }

    /// <summary>
    /// Finder næste unikke ID til et subgoal på tværs af alle mål og internships for en bruger.
    /// </summary>
    /// <param name="user">Brugeren hvorfra subgoal IDs skal findes.</param>
    /// <returns>Næste ledige subgoal ID.</returns>
    private int GetNextUniqueSubgoalId(User user)
    {
        var allSubgoalIds = new List<int>();

        foreach (var internship in user.Studentplan.Internship)
        {
            if (internship.Goal != null)
            {
                foreach (var goal in internship.Goal)
                {
                    if (goal.Subgoals != null)
                    {
                        allSubgoalIds.AddRange(goal.Subgoals.Select(s => s.SubgoalID));
                    }
                }
            }
        }

        return allSubgoalIds.Any() ? allSubgoalIds.Max() + 1 : 1;
    }

    public async Task DeleteSubgoalFromGoal(int userId, int internshipId, int goalId, int subgoalId)
    {
        var user = await _userCollection.Find(u => u._id == userId).FirstOrDefaultAsync();
        if (user == null)
            throw new Exception("Bruger ikke fundet");

        if (user.Studentplan?.Internship == null)
            throw new Exception("Ingen praktikforløb fundet på brugerens elevplan");

        var internship = user.Studentplan.Internship.FirstOrDefault(i => i._id == internshipId);
        if (internship == null)
            throw new Exception("Praktikperiode ikke fundet");

        var goal = internship.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
            throw new Exception("Målet blev ikke fundet i den valgte praktikperiode");

        var subgoal = goal.Subgoals?.FirstOrDefault(sg => sg.SubgoalID == subgoalId);
        if (subgoal == null)
            throw new Exception("Delmål ikke fundet i målet");

        goal.Subgoals.Remove(subgoal);

        var updateResult = await _userCollection.ReplaceOneAsync(u => u._id == userId, user);
        if (!updateResult.IsAcknowledged || updateResult.ModifiedCount == 0)
            throw new Exception("Fejl under gemning af opdateret brugerdata");

        Console.WriteLine($"✅ Subgoal {subgoalId} slettet for bruger {userId}");
    }

    public async Task UpdateSubgoalFromGoal(int userId, int internshipId, int goalId, int subgoalId, Subgoal updatedSubgoal)
    {
        var filter = Builders<User>.Filter.Eq(u => u._id, userId);
        var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

        if (user == null)
            throw new Exception("User not found");

        if (user.Studentplan?.Internship == null)
            throw new Exception("No internships found for user");

        var internship = user.Studentplan.Internship.FirstOrDefault(i => i._id == internshipId);
        if (internship == null)
            throw new Exception("Internship not found");

        var goal = internship.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
            throw new Exception("Goal not found in selected internship");

        var subgoal = goal.Subgoals?.FirstOrDefault(s => s.SubgoalID == subgoalId);
        if (subgoal == null)
            throw new Exception("Subgoal not found in selected goal");

        subgoal.Name = updatedSubgoal.Name;
        subgoal.Date = updatedSubgoal.Date;
        subgoal.Responsible = updatedSubgoal.Responsible;
        subgoal.Deadline = updatedSubgoal.Deadline;
        subgoal.Status = updatedSubgoal.Status;
        subgoal.Approval = updatedSubgoal.Approval;

        if (updatedSubgoal.Comments != null)
            subgoal.Comments = updatedSubgoal.Comments;

        var updateResult = await _userCollection.ReplaceOneAsync(filter, user);
        if (!updateResult.IsAcknowledged || updateResult.ModifiedCount == 0)
            throw new Exception("Failed to update user in database");
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

        if (filter.IsActive.HasValue)
            mongoFilter &= builder.Eq(u => u.IsActive, filter.IsActive.Value);

        var results = await _userCollection.Find(mongoFilter).ToListAsync();
        return results.ToArray();
    }
}
