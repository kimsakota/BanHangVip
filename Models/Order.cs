using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BanHangVip.Models
{
    public enum OrderStatus
    {
        ChoGiao, // Pending
        DaGiao   // Delivered
    }

    public enum HistoryType
    {
        NhapHang, // Import
        XuatHang  // Export/Delivery
    }

    public class SeafoodItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Image { get; set; }
        public string ColorHex { get; set; }
    }

    public partial class OrderItem : ObservableObject
    {
        public SeafoodItem Item { get; set; }

        [ObservableProperty]
        private double _weight;

        public string DisplayWeight => $"{Weight:F2} kg";
    }

    public partial class Order : ObservableObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ObservableProperty]
        private OrderStatus _status;

        public ObservableCollection<OrderItem> Items { get; set; } = new();

        public string StatusColor => Status == OrderStatus.ChoGiao ? "#FF9800" : "#4CAF50"; // Cam / Xanh lá
        public string StatusText => Status == OrderStatus.ChoGiao ? "CHỜ GIAO" : "ĐÃ GIAO";
        public double TotalWeight => Items.Sum(i => i.Weight);
    }

    public class HistoryRecord
    {
        public string Id { get; set; } // Mã đơn hoặc phiếu nhập
        public DateTime Date { get; set; }
        public HistoryType Type { get; set; }
        public string Description { get; set; } // Tên khách hoặc Nguồn hàng
        public double TotalWeight { get; set; }
        public string TypeColor => Type == HistoryType.NhapHang ? "#2196F3" : "#4CAF50"; // Xanh dương / Xanh lá
        public string TypeText => Type == HistoryType.NhapHang ? "NHẬP" : "XUẤT";
    }
}