﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.Views;

namespace SendTracker.ViewModel;

[QueryProperty(nameof(Id), "routeid")]
public partial class RoutePageViewModel : ObservableObject {
    private Route route;

    public string SendName { get; set; } = null;
    public string ClimbType { get; set; } = null;
    public string Grade { get; set; } = null;
    public string Technique { get; set; } = null;
    public string Attempts { get; set; } = null;
    public string Duration { get; set; } = null;
    public int Pitches { get; set; } = 1;

    public int Id { get; set; }

    public async Task<Route> LoadData() {
        return await LoadCurrentRoute();
    }

    private async Task<Route> LoadCurrentRoute() {
        route = await RouteHandler.GetRoute(Id);
        return route;
    }

    [RelayCommand]
    public async Task DeleteCurrentRoute() {
        string action = await Shell.Current.CurrentPage.DisplayActionSheet("Delete Route", "Cancel", "Delete");
        if (action.Equals("Delete")) {
            await StorageHandler.DeletePhoto(route.PhotoPath);
            await RouteHandler.DeleteRoute(route.Id);
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    public async Task EditCurrentRoute() {
        await Shell.Current.GoToAsync($"{nameof(NewRoutePage)}?Id={Id}");
    }
}