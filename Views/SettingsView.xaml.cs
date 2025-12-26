using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}