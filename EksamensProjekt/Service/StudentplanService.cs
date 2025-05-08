using System.Net.Http.Json;
using Core;

namespace EksamensProjekt.Service;

public class StudentplanService : IStudentplan
{
    
    private string serverUrl = "http://localhost:5186";

    private HttpClient client;
    
    public StudentplanService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<Studentplan[]?> GetStudentplanById(int id)
    {
        return await client.GetFromJsonAsync<Studentplan[]>($"{serverUrl}/api/Studentplan/{id}");
    }
}