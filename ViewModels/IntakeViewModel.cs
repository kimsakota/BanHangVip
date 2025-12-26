using BanHangVip.Models;
using BanHangVip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.ViewModels
{
    public partial class IntakeViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        [ObservableProperty]
        private ObservableCollection<Product> products;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsProductSelected))]
        private Product selectedProduct;

        [ObservableProperty]
        private double? inputWeight;

        [ObservableProperty]
        private decimal? inputPrice;

        public bool IsProductSelected => SelectedProduct != null;

        public IntakeViewModel(IDataService dataService)
        {
            Title = "Nhập hàng";
            _dataService = dataService;
            Products = _dataService.GetProducts();
        }

        [RelayCommand]
        private void SelectProduct(Product product)
        {
            SelectedProduct = product;
            // Reset input fields khi chọn sản phẩm mới nếu cần
            InputWeight = null;
            InputPrice = null;
        }

        [RelayCommand]
        private async Task SaveIntake()
        {
            if (SelectedProduct == null || InputWeight == null || InputWeight <= 0)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng chọn sản phẩm và nhập số lượng hợp lệ.", "OK");
                return;
            }

            var historyItem = new HistoryItem
            {
                Id = $"INT-{DateTime.Now.Ticks}",
                Type = "INTAKE",
                ProductName = SelectedProduct.Name,
                Weight = InputWeight.Value,
                Price = InputPrice ?? 0,
                Timestamp = DateTime.Now
            };

            _dataService.AddHistoryItem(historyItem);

            // Hiển thị thông báo thành công (Toast hoặc Alert)
            await Shell.Current.DisplayAlert("Thành công", $"Đã nhập {InputWeight}kg {SelectedProduct.Name} vào kho.", "OK");

            // Reset form
            SelectedProduct = null;
            InputWeight = null;
            InputPrice = null;
        }
    }
}
