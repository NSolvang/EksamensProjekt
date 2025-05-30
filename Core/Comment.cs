using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty; 

    public string Text { get; set; } = "";
    public string UserName { get; set; } = "";
    public string UserRole { get; set; } = "";
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int SubgoalID { get; set; }
}