using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class OrderDetailView : ContentPage
{
    public OrderDetailView(OrderDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}