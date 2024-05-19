namespace PowerTradeService.Models
{
    public class PowerTradeResult
    {
        public DateTime Date { get; set; }
        public int Hour { get; set; }   
        public double TotalVolume { get; set; }
    }
}
