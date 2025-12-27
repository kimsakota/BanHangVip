using BanHangVip.Models;
using BanHangVip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BanHangVip.ViewModels;

public partial class ManageCustomersViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Customer> customers;

    public ManageCustomersViewModel(IDataService dataService)
    {
        Title = "Quản lý Khách hàng";
        _dataService = dataService;
        LoadCustomers();
    }

    private void LoadCustomers()
    {
        Customers = _dataService.GetCustomers();
    }

    [RelayCommand]
    private async Task AddCustomer()
    {
        // 1. Nhập tên
        string name = await Shell.Current.DisplayPromptAsync("Thêm khách hàng", "Nhập tên khách hàng:");
        if (string.IsNullOrWhiteSpace(name)) return;

        // 2. Nhập số điện thoại (Tuỳ chọn)
        string phone = await Shell.Current.DisplayPromptAsync("Thêm khách hàng", "Nhập số điện thoại (nếu có):", keyboard: Keyboard.Telephone);

        // 3. Tạo khách hàng mới
        var newCustomer = new Customer
        {
            Id = Guid.NewGuid().ToString(),
            Name = name.Trim(),
            Phone = phone?.Trim() ?? "",
            Avatar = name.Trim().Substring(0, 1).ToUpper()
        };

        _dataService.AddCustomer(newCustomer);
    }

    [RelayCommand]
    private async Task DeleteCustomer(Customer customer)
    {
        if (customer == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa khách hàng {customer.Name}?", "Xóa", "Hủy");
        if (confirm)
        {
            _dataService.DeleteCustomer(customer);
        }
    }

}