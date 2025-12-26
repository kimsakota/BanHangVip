using BanHangVip.ViewModels;
namespace BanHangVip.Views;
public partial class ImportPage : ContentPage
{
    public ImportPage(ImportViewModel vm) { InitializeComponent(); BindingContext = vm; }
}