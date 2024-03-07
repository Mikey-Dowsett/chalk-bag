using SendTracker.Data;
using SendTracker.Models;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class NewRoutePage {
    private readonly string[] fontGrades = {
        "f3", "f4-", "f4+", "f5-", "f5+", "f6a", "f6a+", "f6b", "f6b+",
        "f6c", "f6c+", "f7a", "f7a+", "f7b", "f7b+", "f7c", "f7c+",
        "f8a", "f8a+", "f8b", "f8b+", "f8c", "f8c+"
    };

    private readonly string[] frenchGrades = {
        "1", "2", "3", "4a", "4b", "4c", "5a", "5b", "5c", "6a",
        "6a+", "6b", "6b+", "6c", "6c+", "7a", "7a+", "7b", "7b+",
        "7c", "7c+", "8a", "8a+", "8b", "8b+", "8c", "8c+", "9a",
        "9a+", "9b", "9b+"
    };

    private readonly string[] uiaaGrades = {
        "I", "II", "III", "IV", "V", "IV+", "V", "V+", "VI-",
        "VI", "VI+", "VII-", "VII", "VII+", "VIII-", "VIII",
        "VIII+", "IX-", "IX", "IX+", "X-", "X", "X+", "XI-",
        "XI", "XI+", "XII-", "XII", "XII+"
    };

    private readonly string[] vGrades = {
        "VB", "V0", "V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8",
        "V9", "V10", "V11", "V12", "V13", "V14", "V15", "V16", "V17"
    };

    private readonly NewRouteViewModel vm;

    private readonly string[] ydsGrades = {
        "5.6", "5.7", "5.8", "5.9", "5.10a", "5.10b", "5.10c",
        "5.10d", "5.11a", "5.11b", "5.11c", "5.11d", "5.12a",
        "5.12b", "5.12c", "5.12d", "5.13a", "5.13b", "5.13c",
        "5.13d", "5.14a", "5.14b", "5.14c", "5.14d", "5.15a",
        "5.15b", "5.15bc", "5.15d"
    };

    public NewRoutePage(NewRouteViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;

        ClimbType.SelectedIndexChanged += (sender, args) => SelectGrade();
    }

    protected override async void OnAppearing() {
        if (vm.RouteId == 0) {
            RouteName.Text = null;
            RouteDescription.Text = null;
            ClimbType.SelectedIndex = 3;
            vm.PhotoPath = null;
            vm.OptionsVisible = false;
            SelectGrade();
        }
        else {
            LoadRoute();
        }
    }

    private async Task LoadRoute() {
        Title = "Edit Route";
        RoutesDatabase database = new();
        Route route = await database.GetRouteAsync(vm.RouteId);
        RouteName.Text = route.SendName;
        RouteDescription.Text = route.Notes;
        ClimbType.SelectedItem = route.ClimbType;
        Technique.SelectedItem = route.Technique;
        Attempts.SelectedItem = route.Attempts;
        RockType.SelectedItem = route.RockType;

        vm.Proposed = route.Proposed;
        vm.Pitches = route.Pitches;
        vm.Duration = route.Duration;
        vm.Falls = route.Falls;
        vm.Rests = route.Rests;
        vm.PhotoPath = route.PhotoPath;
        if (vm.PhotoPath != null) vm.MediaText = "Media Added";
        vm.OptionsVisible = false;

        SelectGrade();
        if (BoulderParent.IsVisible) BoulderGrades.SelectedItem = vm.Proposed ? route.Grade.Trim('*') : route.Grade;
        else if (TallWallParent.IsVisible)
            TallWallGrades.SelectedItem = vm.Proposed ? route.Grade.Trim('*') : route.Grade;
    }

    private void SelectGrade() {
        BoulderParent.IsVisible = false;
        TallWallParent.IsVisible = false;
        if (ClimbType.SelectedItem.Equals("Boulder") || ClimbType.SelectedItem.Equals("Highball")) {
            BoulderParent.IsVisible = true;
            switch (Preferences.Default.Get("boulder_grade", 0)) {
                case 0:
                    BoulderGrades.ItemsSource = vGrades;
                    BoulderGrades.SelectedIndex = 6;
                    break;
                case 1:
                    BoulderGrades.ItemsSource = fontGrades;
                    BoulderGrades.SelectedIndex = 6;
                    break;
            }
        }
        else {
            TallWallParent.IsVisible = true;
            switch (Preferences.Default.Get("tall_wall_grade", 0)) {
                case 0:
                    TallWallGrades.ItemsSource = ydsGrades;
                    TallWallGrades.SelectedIndex = 5;
                    break;
                case 1:
                    TallWallGrades.ItemsSource = frenchGrades;
                    TallWallGrades.SelectedIndex = 5;
                    break;
                case 2:
                    TallWallGrades.ItemsSource = uiaaGrades;
                    TallWallGrades.SelectedIndex = 5;
                    break;
            }
        }
    }

    private void TimeInput_OnTextChanged(object? sender, TextChangedEventArgs e) {
        if (TimeInput.Text.Length == 4 || TimeInput.Text.Length == 7) TimeInput.CursorPosition++;
    }
}