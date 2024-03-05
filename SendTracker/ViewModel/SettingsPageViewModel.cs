using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace SendTracker.ViewModel;

public partial class SettingsPageViewModel : ObservableObject {
    [ObservableProperty] private int boulderGrade;
    [ObservableProperty] private int tallWallGrade;

    [RelayCommand]
    private async Task Save() {
        Preferences.Default.Set("tall_wall_grade", tallWallGrade);
        Preferences.Default.Set("boulder_grade", boulderGrade);

        CancellationTokenSource cancellationToken = new CancellationTokenSource();
        var toast = Toast.Make("Settings Saved!", ToastDuration.Short, 25);
        await toast.Show(cancellationToken.Token);
    }
}