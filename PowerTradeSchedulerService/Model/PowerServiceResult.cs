namespace PowerTradeSchedulerService.Model;

public class PowerServiceResult
{
    public DateTime Date { get; set; }
    public int Hour { get; set; }
    public double TotalVolume { get; set; }
}
