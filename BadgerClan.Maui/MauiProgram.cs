using Microsoft.Extensions.Logging;
using BadgerClan.Maui.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BadgerClan.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

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
    }
}
