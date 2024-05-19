

using PowerTradeSchedulerService.Model;

namespace PowerTradeSchedulerService
{
    public interface IFileGenerationService
    {
        string GenerateCsvContent(List<PowerServiceResult> volumeResults);
        string GenerateCsvFileName(DateTime localNow);
        bool SaveCsvFile(string content, string fileName, string fileLocation);
    }
}
