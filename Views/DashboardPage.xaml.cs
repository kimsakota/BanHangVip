using BanHangVip.ViewModels;

namespace BanHangVip.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}