using Microsoft.Extensions.Logging;
using BadgerClan.Maui.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using BadgerClan.Maui.ViewModels;
using BadgerClan.Maui.Views;
using CommunityToolkit.Maui;
using Grpc.Net.Client;
using BadgerClan.Shared;
using ProtoBuf.Grpc.Client;
using Microsoft.Extensions.Configuration;

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
            baseAddress = "http://10.0.2.2:5140";
        }
        else
        {
            baseAddress = "http://localhost:5140";
        }

        builder.Services.AddSingleton<GrpcStrategyClient>();

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
        builder.Services.AddTransient<GrpcTest>();
        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<GameControllerViewModel>();
        builder.Services.AddTransient<GrpcTestViewModel>();
        return builder;
    }
}

public class GrpcStrategyClient : IDisposable
{
#if DEBUG
    private const string GrpcApiAddress = "http://localhost:5000";
#else
    private const string GrpcApiAddress = "http://localhost:5001";
#endif

    private GrpcChannel channel;
    public IStrategyService Client { get; }

    public GrpcStrategyClient(IConfiguration config)
    {
        GrpcClientFactory.AllowUnencryptedHttp2 = true;
        channel = GrpcChannel.ForAddress(GrpcApiAddress);
        Client = channel.CreateGrpcService<IStrategyService>();
    }
    public void Dispose()
    {
        channel.Dispose();
    }
}
