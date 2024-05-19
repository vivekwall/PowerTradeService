using PowerTradeService.Models;

namespace PowerTradeService.Services
{
    public interface IFileGenerationService
    {
        string GenerateCsvContent(List<PowerTradeResult> volumeResults);
        string GenerateCsvFileName(DateTime localNow);
        bool SaveCsvFile(string content, string fileName, string fileLocation);
    }
}
