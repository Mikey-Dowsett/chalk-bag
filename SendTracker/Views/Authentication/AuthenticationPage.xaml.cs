using SendTracker.Data;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class AuthenticationPage : ContentPage {
    private readonly uint animTime = 150;

    public AuthenticationPage(AuthenticationPageViewModel vm) {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override async void OnAppearing() {
        SessionHandler sessionHandler = new();
        SessionHandler.UserSession = sessionHandler.LoadSession();
        if (SessionHandler.UserSession != null) Application.Current!.MainPage = new AppShell();
    }

    private async void ChangeToCreateAccount(object? sender, EventArgs e) {
        await Login.TranslateTo(-1000, Login.TranslationY, animTime, Easing.SinInOut);
        Login.IsVisible = false;
        Signup.IsVisible = true;
        await Signup.TranslateTo(0, Signup.TranslationY, animTime, Easing.SinInOut);
    }

    private async void ChangeToCreateLogin(object? sender, EventArgs e) {
        await Signup.TranslateTo(1000, Signup.TranslationY, animTime, Easing.SinInOut);
        Login.IsVisible = true;
        Signup.IsVisible = false;
        await Login.TranslateTo(0, Login.TranslationY, animTime, Easing.SinInOut);
    }
}