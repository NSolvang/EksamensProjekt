namespace Core;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


public class Studentplan
{
    
    public int _id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Internship> Internship { get; set; }
    
}