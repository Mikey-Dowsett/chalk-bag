namespace SendTracker;

public partial class App : Application {
    public App() {
        InitializeComponent();

        MainPage = new AppShell();

        Current.UserAppTheme = AppTheme.Light;
        RequestedThemeChanged += (s, e) => { Current.UserAppTheme = AppTheme.Light; };
    }
}