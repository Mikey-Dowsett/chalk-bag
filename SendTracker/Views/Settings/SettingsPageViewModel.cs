using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Views;

namespace SendTracker.ViewModel;

public partial class SettingsPageViewModel : ObservableObject {
    [ObservableProperty] private int boulderGrade;
    [ObservableProperty] private int tallWallGrade;

    [RelayCommand]
    private async Task Save() {
        Preferences.Default.Set("tall_wall_grade", tallWallGrade);
        Preferences.Default.Set("boulder_grade", boulderGrade);

        CancellationTokenSource cancellationToken = new();
        IToast toast = Toast.Make("Settings Saved!", ToastDuration.Short, 25);
        await toast.Show(cancellationToken.Token);
    }

    [RelayCommand]
    private async Task Logout() {
        SessionHandler.Logout();
        Application.Current.MainPage = new AuthenticationPage(new AuthenticationPageViewModel());
    }
}