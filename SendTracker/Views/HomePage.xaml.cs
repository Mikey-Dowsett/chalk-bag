using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class HomePage : ContentPage {
    private readonly HomeViewModel vm;

    public HomePage(HomeViewModel _vm) {
        InitializeComponent();
        BindingContext = _vm;
        vm = _vm;
    }

    protected override async void OnAppearing() {
        vm.LoadRoutes();
    }
}