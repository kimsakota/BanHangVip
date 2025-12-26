using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BanHangVip.Models;
using BanHangVip.Services;
using System.Collections.ObjectModel;

namespace BanHangVip.ViewModels;

public partial class ManageProductsViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Product> products;

    public ManageProductsViewModel(IDataService dataService)
    {
        Title = "Quản lý Hải sản";
        _dataService = dataService;
        Products = _dataService.GetProducts();
    }

    [RelayCommand]
    private async Task AddProduct()
    {
        string name = await Shell.Current.DisplayPromptAsync("Thêm sản phẩm", "Tên sản phẩm:");
        if (string.IsNullOrWhiteSpace(name)) return;

        string priceStr = await Shell.Current.DisplayPromptAsync("Thêm sản phẩm", "Giá mặc định (VNĐ):", keyboard: Keyboard.Numeric);
        if (!decimal.TryParse(priceStr, out decimal price)) return;

        var newProduct = new Product
        {
            Name = name,
            Icon = "🐟", // Default icon
            DefaultPrice = price
        };

        // Lưu ý: ObservableCollection trong DataService cần cơ chế đồng bộ tốt hơn hoặc refresh list
        // Ở đây giả định DataService tự update list tham chiếu
        Products.Add(newProduct);
    }
}