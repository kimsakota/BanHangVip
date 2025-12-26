using CommunityToolkit.Mvvm.ComponentModel;

namespace BanHangVip.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        string title;
    }
}