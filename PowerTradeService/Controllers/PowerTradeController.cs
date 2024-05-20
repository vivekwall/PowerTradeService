using Microsoft.AspNetCore.Mvc;
using PowerTradeService.ExternalServiceWrapper;
using PowerTradeService.Services;

namespace PowerTradeService.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerTradeController : ControllerBase
{
    private readonly IPowerServiceWrapper _powerServiceWrapper;
    private readonly ILogger<PowerTradeController> _logger;
 
    public PowerTradeController(IPowerServiceWrapper powerServiceWrapper, ILogger<PowerTradeController> logger)
    {
        _powerServiceWrapper = powerServiceWrapper;
        _logger = logger;
    }

    [HttpGet(Name = "GetTrades")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Calling PowerService");
     
        var trades = await new PowerTradeBusinessService(_powerServiceWrapper).GetTradesForPeriodAsync();

        return Ok(trades);
      
    }
}
