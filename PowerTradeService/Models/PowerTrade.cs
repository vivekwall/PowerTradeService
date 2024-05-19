namespace PowerTradeService.Models;

public class PowerTradeModel
{
    public DateTime Date { get; set; }
    public PowerPeriodModel[] Periods { get; set; }
}
