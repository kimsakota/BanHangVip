using BanHangVip.Models;
using BanHangVip.ViewModels;
using BanHangVip.Views.Popups;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace BanHangVip.Views;

public partial class HomeView : ContentPage
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    public ICommand ProductTappedCommand => new Command<Product>(OnProductTapped);

    private async void OnProductTapped(Product product)
    {
        if (product == null) return;

        // 1. Hiển thị Popup nhập số Kg
        // Tiêu đề: Tên món - Nhập số lượng
        var popup = new BeautifulNumericPopup($"{product.Name}\nNhập số lượng (Kg)");

        // 2. Chờ người dùng nhập và lấy kết quả
        // Lưu ý: Application.Current.MainPage là Page hiện tại đang mở
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);

        // 3. Xử lý kết quả trả về
        if (result is double quantity && quantity > 0)
        {
            // Logic thêm vào giỏ hàng/đơn hàng ở đây
            AddToOrder(product, quantity);
        }
    }

    private void AddToOrder(Product product, double quantity)
    {
        // Ví dụ: Thêm vào danh sách PendingOrders hoặc Order hiện tại
        // Bạn cần thay đổi logic này tùy theo cách bạn quản lý giỏ hàng

        // Ví dụ thông báo (bạn có thể xóa đi sau khi code xong logic thật)
        Application.Current.MainPage.DisplayAlert("Thành công",
            $"Đã thêm {quantity}kg {product.Name} vào đơn hàng.", "OK");

        // TODO: Gọi service hoặc thêm vào ObservableCollection của đơn hàng
    }
}