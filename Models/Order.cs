using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public OrderStatus Status { get; set; } 

        public DateTime CreatedAt { get; set; }

        public bool IsPaid { get; set; }

        public decimal TotalAmount => Items.Sum(i => i.Total);
    }
}
