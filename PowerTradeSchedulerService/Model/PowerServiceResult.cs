using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeSchedulerService.Model
{
    public class PowerServiceResult
    {
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public double TotalVolume { get; set; }
    }
}
