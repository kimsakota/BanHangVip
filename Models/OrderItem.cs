using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.Models
{
    public partial class OrderItem : ObservableObject
    {
        public string ProductId { get; set; } 
        public string ProductName { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total))]
        private double _weight;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total))]
        private decimal _price;

        public decimal Total => (decimal)Weight * Price;
    }
}
