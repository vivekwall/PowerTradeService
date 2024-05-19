using PowerTradeService.Models;

namespace PowerTradeService.ExternalServiceWrapper;

public interface IPowerServiceWrapper
{
    Task<IEnumerable<PowerTradeModel>> GetTradesAsync(DateTime date);
}
