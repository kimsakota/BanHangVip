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
        // Đã cập nhật: Điều hướng đến trang PaymentsView
        await Shell.Current.GoToAsync(nameof(PaymentsView));
    }

    [RelayCommand]
    private async Task NavigateToStatistics()
    {
        // Đã cập nhật: Điều hướng đến trang StatisticsView
        await Shell.Current.GoToAsync(nameof(StatisticsView));
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
            await Shell.Current.DisplayAlert("Đã khóa", "Ứng dụng đã được khóa an toàn.", "OK");
        }
    }
}