using Core;
using System.Threading.Tasks;
namespace EksamensProjekt.Service;

public interface IExport
{
    Task ExportUsersToExcel(User[] users);
}