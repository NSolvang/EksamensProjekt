using Core;
using EksamensProjekt.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories;

/// <summary>
/// Repository der håndterer kommentarer på subgoals i MongoDB.
/// </summary>
public class CommentRepositoryMongodb : ICommentRepository
{
    /// <summary>
    /// MongoDB collection til brugere, hvor kommentarerne ligger inde under user-stucturen.
    /// </summary>
    private readonly IMongoCollection<User> _userCollection;

    /// <summary>
    /// Opretter en ny instans af CommentRepositoryMongodb og forbinder til MongoDB.
    /// </summary>
    public CommentRepositoryMongodb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var database = client.GetDatabase("comwellDB");
        _userCollection = database.GetCollection<User>("users");
    }

    public async Task AddComment(int userId, int internshipId, int goalId, int subgoalId, Comment comment)
    {
        // Genererer et nyt unikt ID til kommentaren og sætter nødvendige properties
        comment.Id = ObjectId.GenerateNewId().ToString();
        comment.SubgoalID = subgoalId;
        comment.CreatedAt = DateTime.UtcNow;

        var user = await _userCollection.Find(u => u._id == userId).FirstOrDefaultAsync();
        if (user == null)
            throw new Exception($"Bruger med id {userId} blev ikke fundet");

        var internship = user.Studentplan?.Internship?.FirstOrDefault(i => i._id == internshipId);
        if (internship == null)
            throw new Exception($"Internship med id {internshipId} blev ikke fundet");

        var goal = internship.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
            throw new Exception($"Goal med id {goalId} blev ikke fundet");

        var subgoal = goal.Subgoals?.FirstOrDefault(sg => sg.SubgoalID == subgoalId);
        if (subgoal == null)
            throw new Exception($"Subgoal med id {subgoalId} blev ikke fundet");

        if (subgoal.Comments == null)
            subgoal.Comments = new List<Comment>();

        subgoal.Comments.Add(comment);

        var updateResult = await _userCollection.ReplaceOneAsync(u => u._id == userId, user);
        if (!updateResult.IsAcknowledged || updateResult.ModifiedCount == 0)
            throw new Exception("Kunne ikke gemme kommentar til databasen");
    }

    public async Task<List<Comment>> GetCommentsBySubgoalId(int userId, int internshipId, int goalId, int subgoalId)
    {
        var user = await _userCollection.Find(u => u._id == userId).FirstOrDefaultAsync();
        if (user == null)
        {
            Console.WriteLine($"Bruger med ID {userId} ikke fundet");
            return new List<Comment>();
        }

        var internship = user.Studentplan?.Internship?.FirstOrDefault(i => i._id == internshipId);
        if (internship == null)
        {
            Console.WriteLine($"Internship {internshipId} ikke fundet");
            return new List<Comment>();
        }

        var goal = internship.Goal?.FirstOrDefault(g => g.GoalId == goalId);
        if (goal == null)
        {
            Console.WriteLine($"Goal {goalId} ikke fundet");
            return new List<Comment>();
        }

        var subgoal = goal.Subgoals?.FirstOrDefault(sg => sg.SubgoalID == subgoalId);
        if (subgoal == null)
        {
            Console.WriteLine($"Subgoal {subgoalId} ikke fundet");
            return new List<Comment>();
        }

        return subgoal.Comments ?? new List<Comment>();
    }
}
