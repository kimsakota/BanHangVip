using BanHangVip.ViewModels;
namespace BanHangVip.Views;
public partial class PosPage : ContentPage
{
    public PosPage(PosViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}