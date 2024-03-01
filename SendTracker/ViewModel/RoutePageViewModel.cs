using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;

namespace SendTracker.ViewModel;

[QueryProperty(nameof(Id), "routeid")]
public partial class RoutePageViewModel : ObservableObject {
    private Route route;

    public string SendName { get; set; } = null;
    public string ClimbType { get; set; } = null;
    public string Grade { get; set; } = null;
    public string Technique { get; set; } = null;
    public string Attempts { get; set; } = null;

    public int Id { get; set; }

    public async Task<Route> LoadData() {
        return await LoadCurrentRoute();
    }

    private async Task<Route> LoadCurrentRoute() {
        RoutesDatabase database = new();
        route = await database.GetRouteAsync(Id);
        return route;
    }

    [RelayCommand]
    public async Task DeleteCurrentRoute() {
        string action = await Shell.Current.CurrentPage.DisplayActionSheet("Delete Route", "Cancel", "Delete");
        if (action.Equals("Delete")) {
            RoutesDatabase database = new();
            await database.DeleteRouteAsync(route);
            await Shell.Current.GoToAsync("..");
        }
    }
}