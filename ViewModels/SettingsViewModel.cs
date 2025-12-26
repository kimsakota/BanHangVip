using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Views;

namespace BanHangVip.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    public SettingsViewModel()
    {
        Title = "Cài đặt";
    }

    [RelayCommand]
    private async Task NavigateToPayments()
    {
        // Điều hướng đến trang Thanh toán (sẽ tạo sau)
        await Shell.Current.DisplayAlert("Thông báo", "Tính năng Thanh toán đang phát triển", "OK");
    }

    [RelayCommand]
    private async Task NavigateToStatistics()
    {
        // Điều hướng đến trang Thống kê (sẽ tạo sau)
        await Shell.Current.DisplayAlert("Thông báo", "Tính năng Thống kê đang phát triển", "OK");
    }

    [RelayCommand]
    private async Task NavigateToManageProducts()
    {
        await Shell.Current.GoToAsync(nameof(ManageProductsView));
    }

    [RelayCommand]
    private async Task NavigateToManageCustomers()
    {
        await Shell.Current.GoToAsync(nameof(ManageCustomersView));
    }

    [RelayCommand]
    private async Task LockApp()
    {
        bool confirm = await Shell.Current.DisplayAlert("Xác nhận", "Bạn có muốn khóa ứng dụng ngay?", "Khóa", "Hủy");
        if (confirm)
        {
            // Logic khóa app (ví dụ: chuyển về trang đăng nhập hoặc hiện popup nhập PIN)
            await Shell.Current.DisplayAlert("Đã khóa", "Ứng dụng đã được khóa an toàn.", "OK");
        }
    }
}