using BanHangVip.Models;
using BanHangVip.Services;
using BanHangVip.Views.Popups; // Đảm bảo đã import namespace của Popup
using CommunityToolkit.Maui.Views; // Cần thiết để gọi ShowPopupAsync
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts; 
using CommunityToolkit.Maui.Core;

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

        // Tính tổng số kg trong giỏ
        public double TotalCartWeight => CurrentCart.Sum(i => i.Weight);

        // Hiện Bottom Sheet nếu giỏ hàng có đồ
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
            if (product == null) return;

            // 1. Gọi Popup đẹp "BeautifulNumericPopup" để nhập số lượng
            var popup = new BeautifulNumericPopup(product.Name);

            // 2. Chờ người dùng nhấn "NHẬP" và lấy kết quả
            // Lưu ý: ShowPopupAsync trả về object?, cần ép kiểu sang double
            var result = await Shell.Current.ShowPopupAsync(popup);

            // 3. Xử lý kết quả trả về
            if (result is double weight && weight > 0)
            {
                AddOrUpdateItem(product, weight);
            }
        }

        private void AddOrUpdateItem(Product product, double weight)
        {
            // Kiểm tra xem món này đã có trong giỏ chưa
            var existingItem = CurrentCart.FirstOrDefault(x => x.ProductId == product.Id);

            if (existingItem != null)
            {
                // Nếu có rồi thì cộng thêm số lượng (kg)
                existingItem.Weight += weight;
            }
            else
            {
                // Nếu chưa có thì tạo mới
                var newItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Weight = weight,
                    Price = product.DefaultPrice
                };
                CurrentCart.Add(newItem);
            }

            // Cập nhật lại số lượng và tổng kg để giao diện thay đổi
            UpdateCartMetrics();
        }

        [RelayCommand]
        private async Task SaveOrder()
        {
            if (CurrentCart.Count == 0) return;

            // --- BẮT ĐẦU SỬA ĐỔI: Sử dụng Popup chọn khách hàng ---

            // 1. Lấy danh sách khách hàng từ DataService
            // (Lưu ý: Bạn cần đảm bảo đã thêm hàm GetCustomers vào IDataService và DataService)
            var customers = _dataService.GetCustomers();

            // 2. Khởi tạo và hiển thị Popup chọn khách
            var popup = new CustomerSelectionPopup(customers);
            var result = await Shell.Current.ShowPopupAsync(popup);

            // 3. Kiểm tra kết quả trả về
            if (result is Customer selectedCustomer)
            {
                // Tạo đơn hàng với thông tin khách đã chọn
                var newOrder = new Order
                {
                    Id = $"DH{DateTime.Now.Ticks.ToString().Substring(10)}",
                    CustomerName = selectedCustomer.Name, // Lấy tên từ đối tượng Customer
                    // Nếu sau này Order có thêm trường Phone, bạn gán: CustomerPhone = selectedCustomer.Phone,
                    Items = new List<OrderItem>(CurrentCart),
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.Now
                };

                // Lưu vào CSDL
                _dataService.AddOrder(newOrder);

                // Dọn dẹp giỏ hàng
                CurrentCart.Clear();
                UpdateCartMetrics();

                // Thông báo thành công kèm tên khách
                //await Shell.Current.DisplayToastAsync($"Đã lưu đơn cho {selectedCustomer.Name}");
            }
            else
            {
                // Trường hợp người dùng bấm đóng popup mà không chọn ai -> Không làm gì cả (Hủy lưu)
            }

            // --- KẾT THÚC SỬA ĐỔI ---
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
            // Thông báo cho giao diện biết các thuộc tính này đã thay đổi
            OnPropertyChanged(nameof(TotalCartWeight));
            OnPropertyChanged(nameof(IsCartVisible));
            OnPropertyChanged(nameof(CurrentCart));
        }
    }
}