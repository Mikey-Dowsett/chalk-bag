using System.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;

namespace SendTracker.ViewModel;

public partial class AuthenticationPageViewModel : ObservableObject, INotifyPropertyChanged {
    [ObservableProperty] private string? confirmPassword;
    [ObservableProperty] private string? email;
    [ObservableProperty] private string? username;

    private bool loginEnabled = true;
    [ObservableProperty] private string? password;

    private bool signupEnabled = true;

    public bool LoginEnabled {
        get => loginEnabled;
        set {
            if (loginEnabled == value) return;
            loginEnabled = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginEnabled)));
        }
    }

    public bool SignUpEnabled {
        get => signupEnabled;
        set {
            if (signupEnabled == value) return;
            signupEnabled = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SignUpEnabled)));
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    [RelayCommand]
    private async Task Login() {
        LoginEnabled = false;
        await SessionHandler.Login(email, password);
        if (SessionHandler.UserSession != null) {
            SessionHandler sessionHandler = new();
            sessionHandler.SaveSession(SessionHandler.UserSession);
            LoadApp();
        }
        else {
            LoginEnabled = true;
            await ShowErrorToast("Invalid Credentials");
        }
    }

    [RelayCommand]
    private async Task SignUp() {
        SignUpEnabled = false;

        if (email == null) {
            await ShowErrorToast("Missing Email");
            return;
        }

        if (password == null || password.Length < 8) {
            await ShowErrorToast("Invalid Password");
            return;
        }

        if (password != confirmPassword) {
            await ShowErrorToast("Passwords don't match");
            return;
        }

        await SessionHandler.SignUp(email, password);
        if (SessionHandler.UserSession != null) {
            await UserHandler.CreateUser(new User(SessionHandler.UserSession.User.Id, username));
            LoadApp();
        }

        SignUpEnabled = true;
    }

    private async Task ShowErrorToast(string message) {
        CancellationTokenSource cancellationToken = new();
        IToast toast = Toast.Make(message, ToastDuration.Short, 25);
        await toast.Show(cancellationToken.Token);
    }

    public void LoadApp() {
        Application.Current!.MainPage = new AppShell();
    }
}