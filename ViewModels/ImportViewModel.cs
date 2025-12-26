using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;

namespace BanHangVip.ViewModels
{
    public partial class ImportViewModel : BaseViewModel
    {
        public ObservableCollection<SeafoodItem> Items { get; } = new();

        [ObservableProperty]
        string note;

        public ImportViewModel()
        {
            Title = "Nhập hàng";
            LoadItems();
        }

        private void LoadItems()
        {
            // Danh sách các mặt hàng có thể nhập
            Items.Add(new SeafoodItem { Name = "Tôm Sú", ColorHex = "#FFB74D" });
            Items.Add(new SeafoodItem { Name = "Cua Thịt", ColorHex = "#E57373" });
            Items.Add(new SeafoodItem { Name = "Mực Lá", ColorHex = "#4DB6AC" });
            Items.Add(new SeafoodItem { Name = "Cá Mú", ColorHex = "#64B5F6" });
        }

        [RelayCommand]
        async Task SaveImport()
        {
            if (string.IsNullOrWhiteSpace(Note))
            {
                bool confirm = await Shell.Current.DisplayAlert("Cảnh báo", "Bạn chưa ghi chú nguồn hàng. Tiếp tục lưu?", "Lưu", "Quay lại");
                if (!confirm) return;
            }

            await Shell.Current.DisplayAlert("Thành công", "Đã lưu phiếu nhập kho", "OK");
            Note = string.Empty; // Reset form
        }
    }
}