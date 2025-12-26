using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;
using BanHangVip.Views;

namespace BanHangVip.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        // Danh sách đơn hàng đang chờ
        public ObservableCollection<Order> Orders { get; } = new();

        // --- Các chỉ số thống kê (Dashboard Stats) ---

        [ObservableProperty]
        int pendingOrderCount;

        [ObservableProperty]
        double totalPendingWeight;

        public DashboardViewModel()
        {
            Title = "Bảng điều khiển";
            LoadData();
        }

        private void LoadData()
        {
            // Giả lập dữ liệu
            var mockOrders = new List<Order>
            {
                new Order
                {
                    CustomerName = "Anh Hùng - Bàn 5",
                    Status = OrderStatus.ChoGiao,
                    CreatedAt = DateTime.Now.AddMinutes(-10),
                    Items = { new OrderItem { Item = new SeafoodItem { Name = "Cua Cà Mau" }, Weight = 2.5 } }
                },
                new Order
                {
                    CustomerName = "Chị Lan - Mang về",
                    Status = OrderStatus.ChoGiao,
                    CreatedAt = DateTime.Now.AddMinutes(-30),
                    Items = { new OrderItem { Item = new SeafoodItem { Name = "Tôm Hùm" }, Weight = 1.2 } }
                },
                new Order
                {
                    CustomerName = "Anh Tuấn - Bàn VIP",
                    Status = OrderStatus.ChoGiao,
                    CreatedAt = DateTime.Now.AddMinutes(-5),
                    Items =
                    {
                        new OrderItem { Item = new SeafoodItem { Name = "Ghẹ Xanh" }, Weight = 3.0 },
                        new OrderItem { Item = new SeafoodItem { Name = "Ốc Hương" }, Weight = 1.0 }
                    }
                }
            };

            Orders.Clear();
            foreach (var order in mockOrders)
            {
                Orders.Add(order);
            }

            // Tính toán lại số liệu thống kê
            RecalculateStats();
        }

        private void RecalculateStats()
        {
            PendingOrderCount = Orders.Count;
            TotalPendingWeight = Orders.Sum(o => o.TotalWeight);
        }

        [RelayCommand]
        async Task GoToCreateOrder()
        {
            await Shell.Current.GoToAsync(nameof(CreateOrderPage));
        }

        [RelayCommand]
        async Task GoToDetail(Order order)
        {
            if (order == null) return;

            var navParam = new Dictionary<string, object>
            {
                { "Order", order }
            };
            await Shell.Current.GoToAsync(nameof(OrderDetailPage), navParam);
        }

        [RelayCommand]
        async Task QuickDeliver(Order order)
        {
            if (order == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Xác nhận", $"Đã giao xong đơn cho {order.CustomerName}?", "Đúng", "Hủy");
            if (confirm)
            {
                Orders.Remove(order);
                // Cập nhật lại thống kê sau khi xóa đơn
                RecalculateStats();

                // Thực tế: Gọi API cập nhật trạng thái đơn hàng và trừ kho
            }
        }

        [RelayCommand]
        void Refresh()
        {
            IsBusy = true;
            // Logic tải lại dữ liệu từ API
            LoadData();
            IsBusy = false;
        }
    }
}