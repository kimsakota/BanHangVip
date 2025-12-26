using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;

namespace BanHangVip.ViewModels
{
    [QueryProperty(nameof(Order), "Order")]
    public partial class OrderDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Order order;

        [RelayCommand]
        async Task ConfirmDelivery()
        {
            if (Order == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Xác nhận", "Xác nhận đã giao hàng và trừ kho?", "Đồng ý", "Hủy");
            if (confirm)
            {
                // Logic cập nhật DB
                Order.Status = OrderStatus.DaGiao;
                await Shell.Current.DisplayAlert("Hoàn tất", "Đơn hàng đã được cập nhật", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        async Task CancelOrder()
        {
            bool answer = await Shell.Current.DisplayAlert("Hủy đơn?", "Bạn có chắc muốn hủy đơn này không? Hành động này không thể hoàn tác.", "Có, hủy đơn", "Không");
            if (answer)
            {
                // Logic hủy đơn
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}