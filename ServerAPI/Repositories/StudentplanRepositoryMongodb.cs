using MongoDB.Driver;
using Core;

namespace ServerAPI.Repositories;

/// <summary>
/// Repository der håndterer adgang til standard studentplan skabelonen i MongoDB.
/// </summary>
public class StudentplanRepositoryMongodb : IStudentplanRepository
{
    /// <summary>
    /// MongoDB collection for studentplan skabeloner.
    /// </summary>
    private readonly IMongoCollection<Studentplan> _templateCollection;

    /// <summary>
    /// Opretter en ny instans af StudentplanRepositoryMongodb og forbinder til MongoDB.
    /// </summary>
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