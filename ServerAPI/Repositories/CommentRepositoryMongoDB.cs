using Core;
using EksamensProjekt.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Repositories;

public class CommentRepositoryMongoDb : ICommentRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public CommentRepositoryMongoDb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var database = client.GetDatabase("comwellDB");
        _userCollection = database.GetCollection<User>("users");
    }

    public async Task AddComment(int userId, int goalId, int subgoalId, Comment comment)
    {
        try
        {
            // Find user
            var user = await _userCollection.Find(u => u.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
                throw new Exception($"User with ID {userId} not found");

            var goal = user.Studentplan?.Goal?.FirstOrDefault(g => g.GoalId == goalId);
            if (goal == null)
                throw new Exception($"Goal with ID {goalId} not found for user {userId}");

            var subgoal = goal.Subgoals?.FirstOrDefault(sg => sg.SubgoalID == subgoalId);
            if (subgoal == null)
                throw new Exception($"Subgoal with ID {subgoalId} not found in goal {goalId}");

            // Assign new ObjectId to comment Id
            comment.Id = ObjectId.GenerateNewId().ToString();
            comment.SubgoalID = subgoalId;
            comment.CreatedAt = DateTime.UtcNow;

            Console.WriteLine($"Adding comment with Id: {comment.Id} for userId={userId}, goalId={goalId}, subgoalId={subgoalId}");

            // Prepare filter to find the user with matching goal and subgoal
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);

            // Update definition with $push to add comment into correct nested array
            var update = Builders<User>.Update.Push("Studentplan.Goal.$[goal].Subgoals.$[subgoal].Comments", comment);

            // Array filters to match goal and subgoal inside nested arrays
            var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("goal.GoalId", goalId)),
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("subgoal.SubgoalID", subgoalId))
            };

            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            var result = await _userCollection.UpdateOneAsync(filter, update, updateOptions);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Failed to update comments array");
            }

            Console.WriteLine("Comment added successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddComment: {ex.Message}");
            throw;
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
