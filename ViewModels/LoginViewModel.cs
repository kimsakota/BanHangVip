using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Views;

namespace BanHangVip.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            if (IsBusy) return;

            // Validate
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin", "OK");
                return;
            }

            IsBusy = true;
            await Task.Delay(1000); // Giả lập call API
            IsBusy = false;

            // Chuyển đến trang chính (AppShell quản lý Tabs)
            Application.Current.MainPage = new AppShell();
        }
    }
}