using PowerTradeService.Models;

namespace PowerTradeService.Services
{
    public interface IPowerTradeBusinessService
    {
        public Task<List<PowerTradeResult>> GetTradesForPeriodAsync();
    }
}
