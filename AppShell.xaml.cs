using BanHangVip.Views;

namespace BanHangVip;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register Routes for navigation
        Routing.RegisterRoute(nameof(CreateOrderPage), typeof(CreateOrderPage));
        Routing.RegisterRoute(nameof(OrderDetailPage), typeof(OrderDetailPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }
}