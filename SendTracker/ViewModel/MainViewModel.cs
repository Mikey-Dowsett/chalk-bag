﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Models;
using SendTracker.Data;

namespace SendTracker.ViewModel;

public partial class MainViewModel : ObservableObject {
    public ObservableCollection<Route> Routes { get; set; } = new();

    public void LoadRoutes() {
        DisplayCurrentRoutes();
    }

    [RelayCommand]
    private async Task OpenNewRoute() {
        await Shell.Current.GoToAsync(nameof(NewRoutePage));
    }

    [RelayCommand]
    private async Task DisplayCurrentRoutes() {
        RoutesDatabase database = new();
        List<Route> newRoutes = await database.GetRoutesAsync();
        Routes.Clear();
        foreach (Route route in newRoutes) {
            Routes.Add(route);
            Console.WriteLine(route.ToString());
        }
        
    }

    [RelayCommand]
    private async Task DeleteData() {
        RoutesDatabase database = new();
        await database.DeleteDatabase();
        DisplayCurrentRoutes();
    }

    [RelayCommand]
    private async Task OpenRoutePage(int id) {
        await Shell.Current.GoToAsync($"{nameof(RoutePage)}?routeid={id}");
    }
}