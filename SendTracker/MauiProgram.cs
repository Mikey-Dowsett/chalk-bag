using Microsoft.Extensions.Logging;
using SendTracker.ViewModel;
using SendTracker.Data;
using SendTracker.Views;

namespace SendTracker;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsPageViewModel>();

        builder.Services.AddTransient<NewRoutePage>();
        builder.Services.AddTransient<NewRouteViewModel>();

        builder.Services.AddTransient<RoutePage>();
        builder.Services.AddTransient<RoutePageViewModel>();

        builder.Services.AddSingleton<RoutesDatabase>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}