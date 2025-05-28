using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core;

public class Location
{
    public int _id { get; set; }
    
    [BsonRequired]
    public string Name { get; set; } = string.Empty;
}