using BanHangVip.Models;
using BanHangVip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        [ObservableProperty]
        private ObservableCollection<Product> products;

        [ObservableProperty]
        private ObservableCollection<OrderItem> currentCart;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCartWeight))]
        [NotifyPropertyChangedFor(nameof(IsCartVisible))]
        private int cartItemCount;

        public double TotalCartWeight => CurrentCart.Sum(i => i.Weight);
        public bool IsCartVisible => CurrentCart.Count > 0;

        public HomeViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Products = _dataService.GetProducts();
            CurrentCart = new ObservableCollection<OrderItem>();
        }

        [RelayCommand]
        private async Task ProductTapped(Product product)
        {
            // Hiển thị Popup nhập cân nặng (Sử dụng DisplayPromptAsync cho đơn giản, hoặc Custom Popup)
            string result = await Shell.Current.DisplayPromptAsync(
                title: product.Name,
                message: $"Nhập khối lượng (kg) cho {product.Name}",
                placeholder: "Ví dụ: 1.5",
                keyboard: Keyboard.Numeric);

            if (double.TryParse(result, out double weight) && weight > 0)
            {
                var newItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Weight = weight,
                    Price = product.DefaultPrice
                };

                CurrentCart.Add(newItem);
                UpdateCartMetrics();
            }
        }

        [RelayCommand]
        private async Task SaveOrder()
        {
            if (CurrentCart.Count == 0) return;

            string customerName = await Shell.Current.DisplayPromptAsync("Lưu đơn", "Nhập tên khách hàng:", initialValue: "Khách lẻ");

            if (string.IsNullOrWhiteSpace(customerName)) return;

            var newOrder = new Order
            {
                Id = $"DH{DateTime.Now.Ticks.ToString().Substring(10)}",
                CustomerName = customerName,
                Items = new List<OrderItem>(CurrentCart),
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _dataService.AddOrder(newOrder);

            CurrentCart.Clear();
            UpdateCartMetrics();

            await Shell.Current.DisplayAlert("Thành công", "Đã lưu đơn hàng chờ giao!", "OK");
        }

        [RelayCommand]
        private void RemoveItem(OrderItem item)
        {
            if (CurrentCart.Contains(item))
            {
                CurrentCart.Remove(item);
                UpdateCartMetrics();
            }
        }

        private void UpdateCartMetrics()
        {
            CartItemCount = CurrentCart.Count;
            OnPropertyChanged(nameof(TotalCartWeight));
            OnPropertyChanged(nameof(IsCartVisible));
        }
    }
}
