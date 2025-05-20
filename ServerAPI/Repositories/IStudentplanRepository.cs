using Core;
namespace ServerAPI.Repositories;

public interface IStudentplanRepository
{
    Task<Studentplan> GetDefaultPlan();
    
}