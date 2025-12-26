using BanHangVip.ViewModels;

namespace BanHangVip
{
    public partial class MainPage : ContentPage
    {
        // Inject HomeViewModel vào constructor
        public MainPage(HomeViewModel vm)
        {
            InitializeComponent(); // Bắt buộc phải có
            BindingContext = vm;   // Gán ViewModel làm BindingContext
        }
    }
}