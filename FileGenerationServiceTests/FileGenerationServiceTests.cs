using PowerTradeSchedulerService;
using PowerTradeSchedulerService.Model;
using System.Text;

namespace FileGenerationServiceTests;

[TestClass]
public class FileGenerationServiceTests
{
    [TestMethod]
    public void GenerateCsvContent_ValidInput_ReturnsExpectedCsv()
    {
        // Arrange
        var service = new FileGenerationService();
        var volumeResults = new List<PowerServiceResult>
    {
        new PowerServiceResult { Hour = 1, TotalVolume = 100 },
        new PowerServiceResult { Hour = 2, TotalVolume = 150 }
    };

        // Act
        var result = service.GenerateCsvContent(volumeResults);

        // Assert
        var expected = new StringBuilder();
        expected.AppendLine("Local Time,Volume");
        expected.AppendLine("01:00,100");
        expected.AppendLine("02:00,150");

        Assert.AreEqual(expected.ToString(), result);
    }

    [TestMethod]
    public void GenerateCsvFileName_ValidDate_ReturnsExpectedFileName()
    {

        // Arrange
        var service = new FileGenerationService();
        var localNow = new DateTime(2024, 5, 19, 14, 30, 0);

        // Act
        var result = service.GenerateCsvFileName(localNow);

        // Assert
        var expected = "PowerPosition_20240519_1430.csv";
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SaveCsvFile_ValidInput_ReturnsTrue()
    {
        // Arrange
        var service = new FileGenerationService();
        var content = "Local Time,Volume\n01:00,100\n02:00,150";
        var fileName = "PowerPosition_20240519_1430.csv";
        var fileLocation = "C:\\temp\\";

        // Ensure the directory exists
        Directory.CreateDirectory(fileLocation);

        // Act
        var result = service.SaveCsvFile(content, fileName, fileLocation);

        // Assert
        Assert.IsTrue(result);

        // Clean up
        File.Delete(Path.Combine(fileLocation, fileName));
    }

    [TestMethod]
    public void SaveCsvFile_InvalidPath_ReturnsFalse()
    {
        // Arrange
        var service = new FileGenerationService();
        var content = "Local Time,Volume\n01:00,100\n02:00,150";
        var fileName = "PowerPosition_20240519_1430.csv";
        var invalidFileLocation = "Z:\\invalid_path\\";

        // Act
        var result = service.SaveCsvFile(content, fileName, invalidFileLocation);

        // Assert
        Assert.IsFalse(result);
    }
}