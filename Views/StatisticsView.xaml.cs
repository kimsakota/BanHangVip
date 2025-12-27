using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class StatisticsView : ContentPage
{
    private readonly StatisticsViewModel _viewModel;

    public StatisticsView(StatisticsViewModel viewModel)
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