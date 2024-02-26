using SendTracker.Views;

namespace SendTracker;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();

        Routing.RegisterRoute(nameof(NewRoutePage), typeof(NewRoutePage));
        Routing.RegisterRoute(nameof(RoutePage), typeof(RoutePage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}