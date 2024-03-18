using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class HomePage : ContentPage {
    private readonly HomePageViewModel vm;

    public HomePage(HomePageViewModel _vm) {
        InitializeComponent();
        BindingContext = _vm;
        vm = _vm;
    }

    protected override async void OnAppearing() {
        await vm.DisplayCurrentRoutes();
    }
}