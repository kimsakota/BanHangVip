using BanHangVip.Views;

namespace BanHangVip;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ManageProductsView), typeof(ManageProductsView));
        Routing.RegisterRoute(nameof(ManageCustomersView), typeof(ManageCustomersView));

        Routing.RegisterRoute(nameof(PaymentsView), typeof(PaymentsView));
        Routing.RegisterRoute(nameof(StatisticsView), typeof(StatisticsView));

        Routing.RegisterRoute(nameof(OrderDetailView), typeof(OrderDetailView));
    }
}