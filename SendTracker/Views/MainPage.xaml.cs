using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class MainPage : ContentPage {
    private readonly MainViewModel vm;

    public MainPage(MainViewModel _vm) {
        InitializeComponent();
        BindingContext = _vm;
        vm = _vm;
    }

    protected override async void OnAppearing() {
        vm.LoadRoutes();
    }
}