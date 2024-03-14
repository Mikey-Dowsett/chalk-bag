using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.Views;

namespace SendTracker.ViewModel;

public partial class HomeViewModel : ObservableObject {
    public ObservableCollection<Route> Routes { get; set; } = new();

    public void LoadRoutes() {
        DisplayCurrentRoutes();
    }

    [RelayCommand]
    private async Task OpenNewRoute() {
        await Shell.Current.GoToAsync($"{nameof(NewRoutePage)}?Id={0}");
    }

    [RelayCommand]
    private async Task DisplayCurrentRoutes() {
        SupabaseSessionHandler sessionHandler = new();
        Routes.Clear();
        List<Route> routes = await sessionHandler.GetRoute();
        foreach (Route route in routes) Routes.Add(route);
    }

    [RelayCommand]
    private async Task OpenRoutePage(int id) {
        await Shell.Current.GoToAsync($"{nameof(RoutePage)}?routeid={id}");
    }
}