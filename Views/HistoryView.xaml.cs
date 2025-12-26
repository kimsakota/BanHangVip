using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class HistoryView : ContentPage
{
    private readonly HistoryViewModel _viewModel;

    public HistoryView(HistoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Refresh dữ liệu mỗi khi vào màn hình này
        _viewModel.Refresh();
    }
}