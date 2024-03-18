using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.Views;

namespace SendTracker.ViewModel;

public partial class HomePageViewModel : ObservableObject {
    public ObservableCollection<DisplayRoute> Routes { get; set; } = new();

    [RelayCommand]
    private async Task OpenNewRoute() {
        await Shell.Current.GoToAsync($"{nameof(NewRoutePage)}?Id={0}");
    }

    [RelayCommand]
    public async Task DisplayCurrentRoutes() {
        Routes.Clear();
        List<Route> routes = await RouteHandler.GetRoute();
        foreach (Route route in routes) {
            MemoryStream memoryStream = await StorageHandler.DownloadPhoto(route.PhotoPath, route.UserId);
            DisplayRoute newRoute = new(route.Id, route.SendName, route.Grade,
                route.AttemptEmoji, route.Notes, route.Date, memoryStream);
            await newRoute.InitializeAsync(route.UserId);
            Routes.Add(newRoute);
        }
    }

    [RelayCommand]
    private async Task OpenRoutePage(int id) {
        await Shell.Current.GoToAsync($"{nameof(RoutePage)}?routeid={id}");
    }
}