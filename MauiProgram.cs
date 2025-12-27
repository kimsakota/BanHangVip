using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using UraniumUI;
using BanHangVip.Services;
using BanHangVip.ViewModels;
using BanHangVip.Views;

namespace BanHangVip;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddMaterialIconFonts();
            });

        // Services
        builder.Services.AddSingleton<IDataService, DataService>();

        // ViewModels
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<PendingOrdersViewModel>();
        builder.Services.AddTransient<IntakeViewModel>();
        builder.Services.AddTransient<HistoryViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<ManageProductsViewModel>();
        builder.Services.AddTransient<ManageCustomersViewModel>();

        builder.Services.AddTransient<PaymentsViewModel>();
        builder.Services.AddTransient<StatisticsViewModel>();
        builder.Services.AddTransient<OrderDetailViewModel>();

        // Views
        builder.Services.AddTransient<HomeView>();
        builder.Services.AddTransient<PendingOrdersView>();
        builder.Services.AddTransient<IntakeView>();
        builder.Services.AddTransient<HistoryView>();
        builder.Services.AddTransient<SettingsView>();
        builder.Services.AddTransient<ManageProductsView>();
        builder.Services.AddTransient<ManageCustomersView>();

        builder.Services.AddTransient<PaymentsView>();
        builder.Services.AddTransient<StatisticsView>();
        builder.Services.AddTransient<OrderDetailView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}