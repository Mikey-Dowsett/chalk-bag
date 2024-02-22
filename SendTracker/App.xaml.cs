namespace SendTracker;

public partial class App : Application {
    public App() {
        InitializeComponent();

        MainPage = new AppShell();
        
        Application.Current.UserAppTheme = AppTheme.Light;
        this.RequestedThemeChanged += (s, e) => { Application.Current.UserAppTheme = AppTheme.Light; };
    }
}