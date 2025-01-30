using Microsoft.Extensions.Logging;
using BadgerClan.Maui.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using BadgerClan.Maui.ViewModels;
using BadgerClan.Maui.Views;
using CommunityToolkit.Maui;

namespace BadgerClan.Maui;

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
            })
            .RegisterViewModels()
            .RegisterViews();

        // to run on Android, base address needs to be 10.0.2.2, otherwise localhost
        string baseAddress;
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            baseAddress = "http://10.0.2.2:2000";
        }
        else
        {
            baseAddress = "http://localhost:2000";
        }


        builder.Services.AddSingleton<IApiService, ApiService>();
        builder.Services.AddHttpClient("GameControllerApi", client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<GameControllerView>();
        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<GameControllerViewModel>();
        return builder;
    }
}
