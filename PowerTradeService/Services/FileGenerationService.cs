using PowerTradeService.Models;
using System.Text;

namespace PowerTradeService.Services
{
    public class FileGenerationService : IFileGenerationService
    {
        public string GenerateCsvContent(List<PowerTradeResult> volumeResults)
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
            throw new NotImplementedException();
        }
    }
}
