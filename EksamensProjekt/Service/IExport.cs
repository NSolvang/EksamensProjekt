using Core;
using System.Threading.Tasks;

namespace EksamensProjekt.Service;

/// <summary>
/// Interface til eksport af brugerdata, f.eks. til Excel.
/// Implementeres af services, der understøtter eksportfunktionalitet.
/// </summary>
public interface IExport
{
    /// <summary>
    /// Eksporterer en samling brugere til en Excel-fil.
    /// </summary>
    /// <param name="users">Array af brugere, der skal eksporteres.</param>
    /// <returns>Task, der signalerer afslutning af eksporten.</returns>
    Task ExportUsersToExcel(User[] users);
}