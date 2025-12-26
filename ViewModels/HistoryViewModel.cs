using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BanHangVip.Models;

namespace BanHangVip.ViewModels
{
    public partial class HistoryViewModel : BaseViewModel
    {
        public ObservableCollection<HistoryRecord> Records { get; } = new();

        [ObservableProperty]
        bool isImportTab = true; // True = Tab Nhập hàng, False = Tab Xuất hàng

        public HistoryViewModel()
        {
            Title = "Lịch sử kho";
            LoadData();
        }

        // Khi property IsImportTab thay đổi, hàm này sẽ tự động chạy (tính năng của MVVM Toolkit)
        partial void OnIsImportTabChanged(bool value)
        {
            LoadData();
        }

        void LoadData()
        {
            Records.Clear();

            // Giả lập dữ liệu
            if (IsImportTab)
            {
                Records.Add(new HistoryRecord { Id = "PN001", Date = DateTime.Now, Type = HistoryType.NhapHang, Description = "Vựa hải sản 79", TotalWeight = 50.5 });
                Records.Add(new HistoryRecord { Id = "PN002", Date = DateTime.Now.AddDays(-1), Type = HistoryType.NhapHang, Description = "Ghe Cậu Ba", TotalWeight = 120.0 });
                Records.Add(new HistoryRecord { Id = "PN003", Date = DateTime.Now.AddDays(-2), Type = HistoryType.NhapHang, Description = "Chợ đầu mối", TotalWeight = 80.0 });
            }
            else
            {
                Records.Add(new HistoryRecord { Id = "DH005", Date = DateTime.Now, Type = HistoryType.XuatHang, Description = "Bàn 5 - Anh Hùng", TotalWeight = 2.5 });
                Records.Add(new HistoryRecord { Id = "DH004", Date = DateTime.Now.AddHours(-1), Type = HistoryType.XuatHang, Description = "Khách mang về", TotalWeight = 1.2 });
                Records.Add(new HistoryRecord { Id = "DH003", Date = DateTime.Now.AddHours(-2), Type = HistoryType.XuatHang, Description = "Bàn VIP 1", TotalWeight = 5.0 });
            }
        }

        [RelayCommand]
        void SwitchTab(string type)
        {
            IsImportTab = (type == "Import");
        }
    }
}