using System.Collections.ObjectModel;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class MainPage : ContentPage {
    private MainViewModel vm;
    public MainPage(MainViewModel _vm) {
        InitializeComponent();
        BindingContext = _vm;
        vm = _vm;
    }

    protected override async void OnAppearing() {
        vm.LoadRoutes();
    }
}