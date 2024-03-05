using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class NewRoutePage {
    private NewRouteViewModel vm;
    public NewRoutePage(NewRouteViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;

        ClimbType.SelectedIndexChanged += (sender, args) => SelectGrade();
    }

    protected override async void OnAppearing() {
        RouteName.Text = null;
        RouteDescription.Text = null;
        ClimbType.SelectedIndex = 3;
        Technique.SelectedIndex = 0;
        Attempts.SelectedIndex = 0;
        RockType.SelectedIndex = 0;
        vm.PhotoPath = null;
        vm.OptionsVisible = false;
        vm.MediaVisible = false;
        SelectGrade();
    }

    private void SelectGrade() {
        YDSParent.IsVisible = false;
        FrenchParent.IsVisible = false;
        UIAAParent.IsVisible = false;
        VGradeParent.IsVisible = false;
        FontParent.IsVisible = false;

        if (ClimbType.SelectedItem.Equals("Boulder") || ClimbType.SelectedItem.Equals("Highball"))
            switch (Preferences.Default.Get("boulder_grade", 0)) {
                case 0:
                    VGradeParent.IsVisible = true;
                    VGrade.SelectedIndex = 4;
                    break;
                case 1:
                    FontParent.IsVisible = true;
                    Font.SelectedIndex = 7;
                    break;
            }
        else
            switch (Preferences.Default.Get("tall_wall_grade", 0)) {
                case 0:
                    YDSParent.IsVisible = true;
                    YDS.SelectedIndex = 6;
                    break;
                case 1:
                    FrenchParent.IsVisible = true;
                    French.SelectedIndex = 11;
                    break;
                case 2:
                    UIAAParent.IsVisible = true;
                    UIAA.SelectedIndex = 9;
                    break;
            }
    }

    private void TimeInput_OnTextChanged(object? sender, TextChangedEventArgs e) {
        if (TimeInput.Text.Length == 4 || TimeInput.Text.Length == 7) TimeInput.CursorPosition++;
    }
}