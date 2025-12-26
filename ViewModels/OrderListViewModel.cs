using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;
using BanHangVip.Views;

namespace BanHangVip.ViewModels
{
    public partial class OrderListViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; } = new();

        public OrderListViewModel()
        {
            Title = "Đơn chờ giao";
            LoadMockData();
        }

        private void LoadMockData()
        {
            Orders.Add(new Order
            {
                CustomerName = "Anh Hùng - Bàn 5",
                Status = OrderStatus.ChoGiao,
                CreatedAt = DateTime.Now.AddMinutes(-10),
                Items = { new OrderItem { Item = new SeafoodItem { Name = "Cua Cà Mau" }, Weight = 2.5 } }
            });

            Orders.Add(new Order
            {
                CustomerName = "Chị Lan - Mang về",
                Status = OrderStatus.ChoGiao,
                CreatedAt = DateTime.Now.AddMinutes(-30),
                Items = { new OrderItem { Item = new SeafoodItem { Name = "Tôm Hùm" }, Weight = 1.2 } }
            });
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
                // Thực tế: Gọi API cập nhật trạng thái đơn hàng và trừ kho
            }
        }
    }
}