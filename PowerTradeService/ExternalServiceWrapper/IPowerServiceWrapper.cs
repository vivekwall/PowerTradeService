using PowerTradeService.Models;
using Services;

namespace PowerTradeService.ExternalServiceWrapper
{
    public interface IPowerServiceWrapper
    {
        Task<IEnumerable<PowerTradeModel>> GetTradesAsync(DateTime date);
    }
}
