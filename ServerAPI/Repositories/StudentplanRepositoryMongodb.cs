using MongoDB.Driver;
using Core;
namespace ServerAPI.Repositories;

public class StudentplanRepositoryMongodb : IStudentplanRepository
{
    private readonly IMongoCollection<Studentplan> _templateCollection;

    public StudentplanRepositoryMongodb()
    {
        var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
        var db = client.GetDatabase("comwellDB");
        _templateCollection = db.GetCollection<Studentplan>("studentplan_template");
    }

    public async Task<Studentplan> GetDefaultPlan()
    {
        // Forventer kun én skabelon – ellers find med filter
        return await _templateCollection.Find(FilterDefinition<Studentplan>.Empty).FirstOrDefaultAsync();
    }
}