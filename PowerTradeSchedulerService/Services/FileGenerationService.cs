using PowerTradeSchedulerService.Model;
using System.Text;

namespace PowerTradeSchedulerService;

public class FileGenerationService : IFileGenerationService
{
    public string GenerateCsvContent(List<PowerServiceResult> volumeResults)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Local Time,Volume");

        foreach (var result in volumeResults)
        {
            string localTime = $"{result.Hour:D2}:00";
            csvBuilder.AppendLine($"{localTime},{result.TotalVolume}");
        }

        return csvBuilder.ToString();
    }

    public string GenerateCsvFileName(DateTime localNow)
    {
        string datePart = localNow.ToString("yyyyMMdd");
        string timePart = localNow.ToString("HHmm");
        return $"PowerPosition_{datePart}_{timePart}.csv";
    }

    public bool SaveCsvFile(string content, string fileName, string fileLocation)
    {
        try
        {
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }
            
            string fullPath = $"{fileLocation}{fileName}";
            File.WriteAllText(fullPath, content);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
