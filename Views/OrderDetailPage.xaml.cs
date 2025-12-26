using BanHangVip.ViewModels;
namespace BanHangVip.Views;
public partial class OrderDetailPage : ContentPage
{
    public OrderDetailPage(OrderDetailViewModel vm) { InitializeComponent(); BindingContext = vm; }
}