using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class ManageProductsView : ContentPage
{
    public ManageProductsView(ManageProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}