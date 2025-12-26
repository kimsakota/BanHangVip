using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BanHangVip.ViewModels;

public partial class ManageCustomersViewModel : BaseViewModel
{
    // Giả lập danh sách khách hàng vì chưa có CustomerModel
    [ObservableProperty]
    private ObservableCollection<string> customers;

    public ManageCustomersViewModel()
    {
        Title = "Quản lý Khách hàng";
        Customers = new ObservableCollection<string>
        {
            "Nhà Mai", "Nhà Nguyệt", "Nhà Thọ", "Khách lẻ"
        };
    }

    [RelayCommand]
    private async Task AddCustomer()
    {
        string name = await Shell.Current.DisplayPromptAsync("Thêm khách hàng", "Tên khách hàng:");
        if (!string.IsNullOrWhiteSpace(name))
        {
            Customers.Add(name);
        }
    }
}