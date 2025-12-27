using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;
using BanHangVip.Services;
using System.Collections.ObjectModel;

namespace BanHangVip.ViewModels;

// Class phụ để hiển thị thông tin nợ của từng khách
public class CustomerDebt
{
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public int OrderCount { get; set; }
}

public partial class PaymentsViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<CustomerDebt> debts;

    public PaymentsViewModel(IDataService dataService)
    {
        Title = "Công nợ khách hàng";
        _dataService = dataService;
        LoadData();
    }

    public void Refresh()
    {
        LoadData();
    }

    private void LoadData()
    {
        var unpaidOrders = _dataService.GetUnpaidOrders();

        // Gom nhóm theo tên khách hàng
        var grouped = unpaidOrders
            .GroupBy(o => o.CustomerName)
            .Select(g => new CustomerDebt
            {
                CustomerName = g.Key,
                // SỬA LỖI: Tính tổng tiền trực tiếp từ danh sách Items
                // (decimal)i.Weight * i.Price thay vì gọi o.TotalValue
                TotalAmount = g.Sum(o => o.Items.Sum(i => (decimal)i.Weight * i.Price)),
                OrderCount = g.Count()
            })
            .OrderByDescending(d => d.TotalAmount)
            .ToList();

        Debts = new ObservableCollection<CustomerDebt>(grouped);
    }

    [RelayCommand]
    private async Task ProcessPayment(CustomerDebt debt)
    {
        if (debt == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Xác nhận thu tiền",
            $"Khách: {debt.CustomerName}\nTổng thu: {debt.TotalAmount:N0} đ\nTừ {debt.OrderCount} đơn hàng",
            "Xác nhận", "Hủy");

        if (confirm)
        {
            _dataService.ProcessPayment(debt.CustomerName);
            LoadData(); // Tải lại danh sách sau khi thu
            await Shell.Current.DisplayAlert("Thành công", "Đã tất toán công nợ!", "OK");
        }
    }
}