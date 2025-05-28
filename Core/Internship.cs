using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Core;

public class Internship
{
    
    public int _id { get; set; }
    
    public string InternshipName { get; set; }
    
    public List<Goal>? Goal { get; set; }
    
}