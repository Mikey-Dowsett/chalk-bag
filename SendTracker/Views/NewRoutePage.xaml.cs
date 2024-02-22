using SendTracker.ViewModel;

namespace SendTracker;

public partial class NewRoutePage {
    public NewRoutePage(NewRouteViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        
        ClimbType.SelectedIndexChanged += (sender, args) => {
            if (ClimbType.SelectedItem.Equals("Boulder")) {
                VGradeParent.IsVisible = true;
                YDSParent.IsVisible = false;
            }
            else {
                VGradeParent.IsVisible = false;
                YDSParent.IsVisible = true;
            }
        };
    }

    protected override async void OnAppearing() {
        RouteName.Text = null;
        RouteDescription.Text = null;
        ClimbType.SelectedIndex = 0;
        VGrade.SelectedIndex = 5;
        YDS.SelectedIndex = 4;
        Technique.SelectedIndex = 0;
        Attempts.SelectedIndex = 0;
        RockType.SelectedIndex = 0;
    }
}