using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;
using BanHangVip.Services;
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
        // Lấy dữ liệu mới nhất từ service
        PendingOrders = _dataService.GetPendingOrders();
    }

    [RelayCommand]
    private async Task ConfirmDelivery(Order order)
    {
        if (order == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Xác nhận",
            $"Xác nhận giao đơn hàng cho {order.CustomerName}?\nTổng: {order.Items.Count} món",
            "Giao ngay", "Hủy");

        if (confirm)
        {
            // Gọi service xử lý logic nghiệp vụ
            _dataService.DeliverOrder(order);

            // Cập nhật lại danh sách hiển thị
            PendingOrders.Remove(order);

            // Thông báo
            await Shell.Current.DisplayAlert("Thành công", "Đơn hàng đã được xác nhận giao!", "OK");
        }
    }

    [RelayCommand]
    private async Task EditOrder(Order order)
    {
        // Tính năng mở rộng: Điều hướng sang trang chi tiết để sửa giá/cân nặng
        await Shell.Current.DisplayAlert("Thông báo", "Tính năng sửa đơn đang phát triển", "OK");
    }
}