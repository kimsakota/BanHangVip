using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class ManageCustomersView : ContentPage
{
    public ManageCustomersView(ManageCustomersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}