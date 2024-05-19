using PowerTradeService.ExternalServiceWrapper;
using PowerTradeService.Models;

namespace PowerTradeService.Services;

public class PowerTradeBusinessService : IPowerTradeBusinessService
{
    private readonly ILogger<PowerTradeBusinessService> _logger;
    private readonly IPowerServiceWrapper _powerServiceWrapper;
    public PowerTradeBusinessService(IPowerServiceWrapper powerServiceWrapper)
    {
        _powerServiceWrapper = powerServiceWrapper;
       
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole();
        });
        
        _logger = loggerFactory.CreateLogger<PowerTradeBusinessService>();
    }
    public async Task<List<PowerTradeResult>> GetTradesForPeriodAsync()
    {
        var date = DateTime.UtcNow.Date;
        var londonTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
        var londonDate = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Utc, londonTimeZone);
        var londonNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, londonTimeZone);

         _logger.LogInformation($"Fetching power trades for {londonDate:dd/MM/yyyy}");

        var periodStart = TimeZoneInfo.ConvertTimeToUtc(londonDate.AddHours(-1), londonTimeZone);

        var trades = await _powerServiceWrapper.GetTradesAsync(DateTime.UtcNow.Date);

        var totalVolumes = trades
               .SelectMany(trade => trade.Periods.Select(period => new
               {
                   Period = period.Period,
                   Volume = period.Volume,
                   StartTime = periodStart.AddHours(period.Period - 2),  
                   EndTime = periodStart.AddHours(period.Period) 
               }))
               .Select(x => new
               {
                   LocalStartTime = TimeZoneInfo.ConvertTime(x.StartTime, TimeZoneInfo.Utc, londonTimeZone),
                   LocalEndTime = TimeZoneInfo.ConvertTime(x.EndTime, TimeZoneInfo.Utc, londonTimeZone),
                   x.Volume
               })
               .Where(x => x.LocalStartTime <= londonNow) 
               .GroupBy(x => new { x.LocalStartTime.Date, x.LocalStartTime.Hour })
               .Select(group => new PowerTradeResult
               {
                   Date = group.Key.Date,
                   Hour = group.Key.Hour,
                   TotalVolume = group.Sum(x => x.Volume)
               })
               .OrderBy(result => result.Date)
               .ThenBy(result => result.Hour)
               .ToList();

        return totalVolumes;
    }
}
