using Microsoft.Extensions.Logging;
using PowerTradeSchedulerService.Model;
using System.Net.Http.Json;

namespace PowerTradeSchedulerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IFileGenerationService _fileGenerationService;
    private Timer _timer;
    private int _intervalMinutes;
    private TimeZoneInfo _londonTimeZone;
    private readonly HttpClient _httpClient; 
    private readonly IConfiguration _config;
    private readonly string _filePath;
    private readonly string _apiUrl;

    public Worker(ILogger<Worker> logger, IFileGenerationService fileGenerationService, IConfiguration configuration)
    {
        _logger = logger;
        _fileGenerationService = fileGenerationService;
        _config = configuration;
        _httpClient = new HttpClient();
        
        //TODO: Refactor using Configuration Options. 
        _intervalMinutes = int.Parse(_config["ExecutionInterval"]);
        _filePath = _config["FilePath"];
        _apiUrl = _config["PowerTradeServiceUrl"];
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(_intervalMinutes));

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        _logger.LogInformation("Worker executing task.");

        try
        {
            var httpResponse = await _httpClient.GetFromJsonAsync<List<PowerServiceResult>>(_apiUrl);
            var fileName = _fileGenerationService.GenerateCsvFileName(DateTime.UtcNow);
            var content = _fileGenerationService.GenerateCsvContent(httpResponse);

            _fileGenerationService.SaveCsvFile(content, fileName, _filePath);

            _logger.LogInformation("CSV extraction triggered successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while calling the CSV extraction API.");
            _logger.LogError (ex.StackTrace);
        }

    }
    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
    public override void Dispose()
    {
        _timer?.Dispose();
        _httpClient.Dispose();
        base.Dispose();
    }
}
