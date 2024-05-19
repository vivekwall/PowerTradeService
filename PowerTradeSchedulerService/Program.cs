using PowerTradeSchedulerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IFileGenerationService, FileGenerationService>();

var host = builder.Build();
host.Run();
