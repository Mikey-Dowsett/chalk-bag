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

        builder.Services.AddSingleton<AuthenticationPage>();
        builder.Services.AddSingleton<AuthenticationPageViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsPageViewModel>();

        builder.Services.AddTransient<NewRoutePage>();
        builder.Services.AddTransient<NewRouteViewModel>();

        builder.Services.AddTransient<RoutePage>();
        builder.Services.AddTransient<RoutePageViewModel>();

        builder.Services.AddSingleton<AccountPage>();
        builder.Services.AddSingleton<AccountPageViewModel>();

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