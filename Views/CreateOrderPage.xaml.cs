using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class CreateOrderPage : ContentPage
{
    public CreateOrderPage(CreateOrderViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}