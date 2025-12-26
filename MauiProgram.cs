using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<OrderListViewModel>();
        builder.Services.AddTransient<CreateOrderViewModel>();
        builder.Services.AddTransient<OrderDetailViewModel>();
        builder.Services.AddTransient<ImportViewModel>();
        builder.Services.AddTransient<HistoryViewModel>();

        // Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<OrderListPage>();
        builder.Services.AddTransient<CreateOrderPage>();
        builder.Services.AddTransient<OrderDetailPage>();
        builder.Services.AddTransient<ImportPage>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<PosViewModel>();
        builder.Services.AddTransient<PosPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}