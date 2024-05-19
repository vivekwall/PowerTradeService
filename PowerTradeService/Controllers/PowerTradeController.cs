using Microsoft.AspNetCore.Mvc;
using PowerTradeService.Models;
using PowerTradeService.Services;
using Services;
using static Services.PowerService;

namespace PowerTradeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerTradeController : ControllerBase
    {
        private readonly ILogger<PowerTradeBusinessService> _powerTradeBusinessServiceLogger;
        private readonly ILogger<PowerTradeController> _powerTradeControllerLogger;

        public PowerTradeController(ILogger<PowerTradeController> logger)
        {
            _powerTradeControllerLogger = logger;
        }

        [HttpGet(Name = "GetTrades")]
        public async Task<IActionResult> Get()
        {
            var trades = await new PowerTradeBusinessService().GetTradesForPeriodAsync();

            var currentDate = DateTime.UtcNow.Date.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            return Ok(trades);
          
        }
    }
}
