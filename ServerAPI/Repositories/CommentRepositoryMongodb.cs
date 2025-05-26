using Core;
using EksamensProjekt.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories;

public class CommentRepositoryMongodb : ICommentRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public CommentRepositoryMongodb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var database = client.GetDatabase("comwellDB");
        _userCollection = database.GetCollection<User>("users");
    }

    public async Task AddComment(int userId, int goalId, int subgoalId, Comment comment)
    {
        comment.Id = ObjectId.GenerateNewId().ToString();
        comment.SubgoalID = subgoalId;
        comment.CreatedAt = DateTime.UtcNow;

        var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);

        var update = Builders<User>.Update.Push("Studentplan.Goal.$[goal].Subgoals.$[subgoal].Comments", comment);

        var arrayFilters = new[]
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("goal.GoalId", goalId)),
            new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("subgoal.SubgoalID", subgoalId))
        };

        var result = await _userCollection.UpdateOneAsync(
            filter, 
            update, 
            new UpdateOptions { ArrayFilters = arrayFilters }
        );

        if (result.ModifiedCount == 0)
        {
            throw new InvalidOperationException($"Comment could not be added - user {userId}, goal {goalId}, or subgoal {subgoalId} not found");
        }
    }

    public async Task<List<Comment>> GetCommentsBySubgoalId(int userId, int goalId, int subgoalId)
    {
        try
        {
            var user = await _userCollection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
            if (user == null) 
            {
                Console.WriteLine($"User with ID {userId} not found");
                return new List<Comment>();
            }

            var goal = user.Studentplan?.Goal?.FirstOrDefault(g => g.GoalId == goalId);
            if (goal == null) 
            {
                Console.WriteLine($"Goal with ID {goalId} not found for user {userId}");
                return new List<Comment>();
            }

            var subgoal = goal.Subgoals?.FirstOrDefault(sg => sg.SubgoalID == subgoalId);
            if (subgoal == null) 
            {
                Console.WriteLine($"Subgoal with ID {subgoalId} not found in goal {goalId}");
                return new List<Comment>();
            }

            return subgoal.Comments ?? new List<Comment>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetCommentsBySubgoalId: {ex.Message}");
            return new List<Comment>();
        }
    }
}
