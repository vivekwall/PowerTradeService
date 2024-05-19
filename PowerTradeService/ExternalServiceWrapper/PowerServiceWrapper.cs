using PowerTradeService.Models;
using Services;
using static Services.PowerService;

namespace PowerTradeService.ExternalServiceWrapper
{
    public class PowerServiceWrapper : IPowerServiceWrapper
    {
        private readonly PowerService _powerService;
        public PowerServiceWrapper()
        {
            _powerService = new PowerService();
        }

        public async Task<IEnumerable<Models.PowerTradeModel>> GetTradesAsync(DateTime date)
        {
            var trades = await _powerService.GetTradesAsync(date);
            
            return trades.Select(t => new Models.PowerTradeModel
            {
                Date = t.Date,
                Periods = t.Periods.Select(p => new PowerPeriodModel
                {
                    Period = p.Period,
                    Volume = p.Volume
                }).ToArray()
            });
        }
    }
}
