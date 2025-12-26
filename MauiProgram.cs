using BanHangVip.Services;
using BanHangVip.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;


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

        builder.Services.AddSingleton<IDataService, DataService>();


        // ViewModels
        builder.Services.AddTransient<HomeViewModel>();

        // Views
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<MainPage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}