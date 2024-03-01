using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SendTracker.ViewModel;

public partial class SettingsPageViewModel : ObservableObject {
    [ObservableProperty] private int boulderGrade;
    [ObservableProperty] private int tallWallGrade;

    [RelayCommand]
    private async Task Save() {
        Preferences.Default.Set("tall_wall_grade", tallWallGrade);
        Preferences.Default.Set("boulder_grade", boulderGrade);

        await Shell.Current.CurrentPage.DisplayAlert("Saved", "Your settings were successfully saved!", "OK");
    }
}