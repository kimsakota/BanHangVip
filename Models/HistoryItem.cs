using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.Models
{
    public class HistoryItem
    {
        public string Id { get; set; }
        public string Type { get; set; } // INTAKE, DELIVERY, PAYMENT
        public string ProductName { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
