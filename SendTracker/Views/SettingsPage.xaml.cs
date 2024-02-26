using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class SettingsPage : ContentPage {
    public SettingsPage(SettingsPageViewModel vm) {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override async void OnAppearing() {
        TallWallGrade.SelectedIndex = Preferences.Default.Get("tall_wall_grade", 0);
        BoulderGrade.SelectedIndex = Preferences.Default.Get("boulder_grade", 0);
    }
}