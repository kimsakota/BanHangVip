using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;
using BanHangVip.Services;

namespace BanHangVip.ViewModels;

[QueryProperty(nameof(Order), "Order")] // Nhận dữ liệu Order từ điều hướng
public partial class OrderDetailViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private Order order;

    public OrderDetailViewModel(IDataService dataService)
    {
        Title = "Chi tiết đơn hàng";
        _dataService = dataService;
    }

    [RelayCommand]
    private async Task CancelOrder()
    {
        bool confirm = await Shell.Current.DisplayAlert("Hủy đơn", "Bạn có chắc muốn hủy đơn hàng này không?", "Hủy đơn", "Thoát");
        if (confirm)
        {
            // Logic hủy đơn (cần cập nhật thêm trong DataService nếu muốn xóa thật)
            await Shell.Current.DisplayAlert("Đã hủy", "Đơn hàng đã bị hủy.", "OK");
            await Shell.Current.GoToAsync(".."); // Quay lại
        }
    }

    [RelayCommand]
    private async Task PrintInvoice()
    {
        // Giả lập in hóa đơn
        await Shell.Current.DisplayAlert("In hóa đơn", $"Đang gửi lệnh in cho đơn {Order.Id}...", "OK");
    }
}