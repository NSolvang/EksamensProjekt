using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Core;

public class Internship
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public int InternshipId { get; set; }
    
    public string InternshipName { get; set; }
    
    public List<Goal>? Goal { get; set; }
    
}