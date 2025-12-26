using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class IntakeView : ContentPage
{
    public IntakeView(IntakeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}