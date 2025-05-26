using ClosedXML.Excel;
using Core;
using EksamensProjekt.Service;
using Microsoft.JSInterop;

public class ExportService : IExport
{
    private readonly IJSRuntime _jsRuntime;

    public ExportService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ExportUsersToExcel(User[] users)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Elever");

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

        worksheet.Cell(1, 1).Value = "Navn";
        worksheet.Cell(1, 2).Value = "Rolle";
        worksheet.Cell(1, 3).Value = "Lokation";
        worksheet.Cell(1, 4).Value = "Praktikperiode";
        worksheet.Cell(1, 5).Value = "Forløb";
        worksheet.Cell(1, 6).Value = "Status";
        worksheet.Cell(1, 7).Value = "Fremgang";

        for (int i = 0; i < users.Length; i++)
        {
            var user = users[i];
            var row = i + 2;

            worksheet.Cell(row, 1).Value = user.Name ?? "";
            worksheet.Cell(row, 2).Value = user.Role ?? "";
            worksheet.Cell(row, 3).Value = user.Location?.Name ?? "";
            worksheet.Cell(row, 4).Value = user.Internshipyear ?? "";
            worksheet.Cell(row, 5).Value = user.Education ?? "";
            worksheet.Cell(row, 6).Value = user.IsActive ? "Aktiv" : "Inaktiv";
            worksheet.Cell(row, 7).Value = GetUserProgressSummary(user);
        }

        worksheet.Columns().AdjustToContents();

        if (users.Length > 0)
        {
            var dataRange = worksheet.Range(1, 1, users.Length + 1, 7);
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        var fileBytes = stream.ToArray();
        var fileName = $"Elever_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        await DownloadFile(fileName, fileBytes);
    }

    private string GetUserProgressSummary(User user)
    {
        var internships = user.Studentplan?.Internship ?? new List<Internship>();

        var allGoals = internships
            .Where(i => i.Goal != null)
            .SelectMany(i => i.Goal!)
            .ToList();

        if (!allGoals.Any())
            return "Ingen opgaver";

        var allSubgoals = allGoals
            .Where(g => g.Subgoals != null)
            .SelectMany(g => g.Subgoals)
            .Where(s => s != null)
            .ToList();

        if (!allSubgoals.Any())
            return "Ingen delopgaver";

        var totalTasks = allSubgoals.Count;
        var completedTasks = allSubgoals.Count(s =>
            (s.Status?.ToLower() == "færdig") ||
            (s.Status?.ToLower() == "completed") ||
            s.Approval);
        var inProgressTasks = allSubgoals.Count(s =>
            s.Status?.ToLower() == "igang" ||
            s.Status?.ToLower() == "i gang" ||
            s.Status?.ToLower() == "in progress");
        var pendingTasks = allSubgoals.Count(s =>
            s.Status?.ToLower() == "mangler" ||
            s.Status?.ToLower() == "pending" ||
            string.IsNullOrEmpty(s.Status));

        var completionPercentage = totalTasks > 0
            ? (int)Math.Round((completedTasks * 100.0) / totalTasks)
            : 0;

        return $"{completionPercentage}% ({completedTasks}/{totalTasks}) - Færdig: {completedTasks}, I gang: {inProgressTasks}, Mangler: {pendingTasks}";
    }

    private async Task DownloadFile(string filename, byte[] data)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("downloadFileFromByteArray", filename, data);
        }
        catch (JSException jsEx)
        {
            Console.WriteLine($"JavaScript download failed: {jsEx.Message}");
            throw new InvalidOperationException("Failed to download file. Make sure the JavaScript download function is available.", jsEx);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Download failed: {ex.Message}");
            throw;
        }
    }
}
