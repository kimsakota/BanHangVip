using BanHangVip.ViewModels;
namespace BanHangVip.Views;
public partial class HistoryPage : ContentPage
{
    public HistoryPage(HistoryViewModel vm) { InitializeComponent(); BindingContext = vm; }
}