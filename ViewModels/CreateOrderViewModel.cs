using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;

namespace BanHangVip.ViewModels
{
    public partial class CreateOrderViewModel : BaseViewModel
    {
        public ObservableCollection<SeafoodItem> MenuItems { get; } = new();
        public ObservableCollection<string> Customers { get; } = new() { "Khách lẻ", "Bàn 1", "Bàn 2", "Bàn 3", "Bàn VIP", "Mang về" };

        [ObservableProperty]
        Order currentOrder;

        [ObservableProperty]
        SeafoodItem selectedItem;

        [ObservableProperty]
        string weightInput = "0";

        [ObservableProperty]
        bool isPopupVisible;

        public CreateOrderViewModel()
        {
            Title = "Tạo đơn đặt";
            CurrentOrder = new Order { CustomerName = "Khách lẻ" };
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
            MenuItems.Add(new SeafoodItem { Name = "Bạch Tuộc", ColorHex = "#BA68C8", Image = "bachtuoc.png" });
        }

        [RelayCommand]
        void OpenWeightPopup(SeafoodItem item)
        {
            if (item == null) return;
            SelectedItem = item;
            WeightInput = "0";
            IsPopupVisible = true;
        }

        [RelayCommand]
        void ClosePopup()
        {
            IsPopupVisible = false;
            SelectedItem = null;
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
        void QuickAddWeight(string val)
        {
            WeightInput = val;
        }

        [RelayCommand]
        void ConfirmAddItem()
        {
            if (double.TryParse(WeightInput, out double w) && w > 0)
            {
                CurrentOrder.Items.Add(new OrderItem { Item = SelectedItem, Weight = w });
                // Reset popup cho lần nhập sau nhanh hơn nếu cần
                WeightInput = "0";
            }
            ClosePopup();
        }

        [RelayCommand]
        void RemoveItem(OrderItem item)
        {
            if (CurrentOrder.Items.Contains(item))
            {
                CurrentOrder.Items.Remove(item);
            }
        }

        [RelayCommand]
        async Task SaveOrder()
        {
            if (CurrentOrder.Items.Count == 0)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vui lòng chọn ít nhất một món", "OK");
                return;
            }

            // Logic lưu đơn hàng vào Database tại đây
            await Shell.Current.DisplayAlert("Thành công", $"Đã tạo đơn cho {CurrentOrder.CustomerName}", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}