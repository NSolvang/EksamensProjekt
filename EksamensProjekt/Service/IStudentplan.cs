using Core;

namespace EksamensProjekt.Service;

public interface IStudentplan
{
    Task<Studentplan[]?> GetStudentplanById(int id);
    
    
}