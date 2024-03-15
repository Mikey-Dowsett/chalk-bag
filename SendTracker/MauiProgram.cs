using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SendTracker.ViewModel;
using SendTracker.Views;
using Supabase;

namespace SendTracker;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts => {
                fonts.AddFont("Nunito-Regular.ttf", "NunitoRegular");
                fonts.AddFont("Nunito-Bold.ttf", "NunitoBold");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomeViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsPageViewModel>();

        builder.Services.AddTransient<NewRoutePage>();
        builder.Services.AddTransient<NewRouteViewModel>();

        builder.Services.AddTransient<RoutePage>();
        builder.Services.AddTransient<RoutePageViewModel>();

        string url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        string key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
        SupabaseOptions options = new() {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };

        builder.Services.AddSingleton(provider => new Client(url, key, options));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}