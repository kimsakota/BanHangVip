using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;
using BanHangVip.Services;
using BanHangVip.Views; // Đảm bảo import Views
using System.Collections.ObjectModel;

namespace BanHangVip.ViewModels;

public partial class PendingOrdersViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Order> pendingOrders;

    public PendingOrdersViewModel(IDataService dataService)
    {
        Title = "Đơn chờ giao";
        _dataService = dataService;
        LoadData();
    }

    public void Refresh()
    {
        LoadData();
    }

    private void LoadData()
    {
        PendingOrders = _dataService.GetPendingOrders();
    }

    [RelayCommand]
    private async Task ConfirmDelivery(Order order)
    {
        if (order == null) return;
        bool confirm = await Shell.Current.DisplayAlert("Xác nhận", $"Xác nhận giao đơn hàng cho {order.CustomerName}?", "Giao ngay", "Hủy");
        if (confirm)
        {
            _dataService.DeliverOrder(order);
            PendingOrders.Remove(order);
            await Shell.Current.DisplayAlert("Thành công", "Đơn hàng đã được xác nhận giao!", "OK");
        }
    }

    // Lệnh xem chi tiết (Thay thế cho EditOrder cũ)
    [RelayCommand]
    private async Task ViewDetail(Order order)
    {
        if (order == null) return;

        // Truyền object Order sang trang chi tiết
        var navigationParameter = new Dictionary<string, object>
        {
            { "Order", order }
        };
        await Shell.Current.GoToAsync(nameof(OrderDetailView), navigationParameter);
    }
}