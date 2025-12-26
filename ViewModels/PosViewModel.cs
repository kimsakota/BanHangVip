using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;

namespace BanHangVip.ViewModels
{
    public partial class PosViewModel : BaseViewModel
    {
        // Danh sách sản phẩm hiển thị trên lưới
        public ObservableCollection<SeafoodItem> MenuItems { get; } = new();

        // Danh sách khách hàng / Bàn
        public ObservableCollection<string> Customers { get; } = new() { "Khách lẻ", "Bàn 1", "Bàn 2", "Bàn 3", "Bàn 4", "Bàn 5", "Mang về" };

        [ObservableProperty]
        string selectedCustomer = "Khách lẻ";

        // Đơn hàng hiện tại (Giỏ hàng tạm)
        public ObservableCollection<OrderItem> CurrentCart { get; } = new();

        // --- Logic Popup Nhập Kg ---
        [ObservableProperty]
        bool isPopupVisible;

        [ObservableProperty]
        SeafoodItem selectedProduct;

        [ObservableProperty]
        string weightInput = "0";

        // --- Logic Tiện ích Giỏ hàng (Bottom Widget) ---
        [ObservableProperty]
        bool isCartExpanded; // Mở rộng để xem chi tiết xóa món

        [ObservableProperty]
        double totalCartWeight;

        [ObservableProperty]
        int totalCartItems;

        [ObservableProperty]
        bool hasItems; // Để ẩn hiện thanh bottom

        public PosViewModel()
        {
            Title = "Bán Hàng";
            LoadMenu();
        }

        private void LoadMenu()
        {
            MenuItems.Add(new SeafoodItem { Name = "Tôm Sú", ColorHex = "#FFB74D", Image = "tom.png" });
            MenuItems.Add(new SeafoodItem { Name = "Cua Thịt", ColorHex = "#E57373", Image = "cua.png" });
            MenuItems.Add(new SeafoodItem { Name = "Mực Lá", ColorHex = "#4DB6AC", Image = "muc.png" });
            MenuItems.Add(new SeafoodItem { Name = "Nghêu", ColorHex = "#90A4AE", Image = "ngheu.png" });
            MenuItems.Add(new SeafoodItem { Name = "Cá Mú", ColorHex = "#64B5F6", Image = "ca.png" });
            MenuItems.Add(new SeafoodItem { Name = "Ghẹ Xanh", ColorHex = "#BA68C8", Image = "ghe.png" });
            MenuItems.Add(new SeafoodItem { Name = "Ốc Hương", ColorHex = "#FF8A65", Image = "oc.png" });
            MenuItems.Add(new SeafoodItem { Name = "Bạch Tuộc", ColorHex = "#9575CD", Image = "bachtuoc.png" });
            MenuItems.Add(new SeafoodItem { Name = "Hàu Sữa", ColorHex = "#B0BEC5", Image = "hau.png" });
        }

        // --- Xử lý Popup ---
        [RelayCommand]
        void SelectProduct(SeafoodItem item)
        {
            if (item == null) return;
            SelectedProduct = item;
            WeightInput = "0";
            IsPopupVisible = true;
        }

        [RelayCommand]
        void ClosePopup()
        {
            IsPopupVisible = false;
            SelectedProduct = null;
        }

        [RelayCommand]
        void InputNum(string val)
        {
            if (val == "DEL")
            {
                if (WeightInput.Length > 0) WeightInput = WeightInput.Substring(0, WeightInput.Length - 1);
                if (string.IsNullOrEmpty(WeightInput)) WeightInput = "0";
            }
            else if (val == ".")
            {
                if (!WeightInput.Contains(".")) WeightInput += ".";
            }
            else
            {
                if (WeightInput == "0" && val != ".") WeightInput = val;
                else WeightInput += val;
            }
        }

        [RelayCommand]
        void QuickAddWeight(string val) => WeightInput = val;

        [RelayCommand]
        void AddToCart()
        {
            if (double.TryParse(WeightInput, out double w) && w > 0)
            {
                // Thêm vào giỏ
                CurrentCart.Add(new OrderItem { Item = SelectedProduct, Weight = w });
                UpdateCartStats();
            }
            ClosePopup();
        }

        // --- Xử lý Giỏ hàng ---
        [RelayCommand]
        void RemoveItem(OrderItem item)
        {
            CurrentCart.Remove(item);
            UpdateCartStats();
            if (CurrentCart.Count == 0) IsCartExpanded = false;
        }

        [RelayCommand]
        void ToggleCartExpand()
        {
            if (CurrentCart.Count > 0)
                IsCartExpanded = !IsCartExpanded;
        }

        void UpdateCartStats()
        {
            TotalCartItems = CurrentCart.Count;
            TotalCartWeight = CurrentCart.Sum(i => i.Weight);
            HasItems = TotalCartItems > 0;
        }

        [RelayCommand]
        async Task SaveOrder()
        {
            if (CurrentCart.Count == 0) return;

            // Logic: Tạo object Order và Lưu vào DB (Giả lập)
            var newOrder = new Order
            {
                CustomerName = SelectedCustomer,
                Status = OrderStatus.ChoGiao,
                CreatedAt = DateTime.Now
            };
            foreach (var item in CurrentCart) newOrder.Items.Add(item);

            // Thông báo
            await Shell.Current.DisplayAlert("Thành công", $"Đã lưu đơn cho {SelectedCustomer}\nChuyển sang trạng thái chờ giao.", "OK");

            // Reset màn hình để nhập đơn mới ngay lập tức
            CurrentCart.Clear();
            UpdateCartStats();
            SelectedCustomer = "Khách lẻ"; // Reset khách mặc định
            IsCartExpanded = false;
        }
    }
}