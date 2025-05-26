namespace Core;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


public class Studentplan
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public int StudentplanID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Internship> Internship { get; set; }
    
}