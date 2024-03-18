using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class SettingsPage : ContentPage {
    public SettingsPage(SettingsPageViewModel vm) {
        InitializeComponent();

        BindingContext = vm;

        TallWallGrade.SelectedIndex = Preferences.Default.Get("tall_wall_grade", 0);
        BoulderGrade.SelectedIndex = Preferences.Default.Get("boulder_grade", 0);
    }
}