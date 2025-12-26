using BanHangVip.ViewModels;
namespace BanHangVip.Views;
public partial class OrderListPage : ContentPage
{
    public OrderListPage(OrderListViewModel vm) { InitializeComponent(); BindingContext = vm; }
}