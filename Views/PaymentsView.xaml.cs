using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class PaymentsView : ContentPage
{
    private readonly PaymentsViewModel _viewModel;

    public PaymentsView(PaymentsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }
}