using Moq;
using PowerTradeService.ExternalServiceWrapper;
using PowerTradeService.Models;
using PowerTradeService.Services;

namespace PowerServiceUnitTests;

[TestClass]
public class PowerTradeBusinessServiceTests
{
    [TestMethod]
    public async Task GetTradesForPeriodAsync_Test()
    {
        // Arrange
        var mockPowerServiceWrapper = new Mock<IPowerServiceWrapper>();

        mockPowerServiceWrapper.Setup(mock => mock.GetTradesAsync(It.IsAny<DateTime>()))
                               .ReturnsAsync(GetSamplePowerTrades());  

        var businessService = new PowerTradeBusinessService(mockPowerServiceWrapper.Object);

        // Act
        var result = await businessService.GetTradesForPeriodAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count); 
    }

    private List<PowerTradeModel> GetSamplePowerTrades()
    {
        var period = new PowerPeriodModel { Period = 1, Volume = 100 };
        var period1 = new PowerPeriodModel { Period = 2, Volume = 200 };

        return new List<PowerTradeModel>()
        {
            new PowerTradeModel { Date = Convert.ToDateTime("19/05/2024"), Periods = [ period ] },
            new PowerTradeModel { Date = Convert.ToDateTime("19/05/2024"), Periods = [ period1 ] }
        };
    }
}
