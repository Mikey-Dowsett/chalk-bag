using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Models;
using SendTracker.Data;

namespace SendTracker.ViewModel;

public partial class NewRouteViewModel : ObservableObject {
    public string SendName { get; set; }
    public string ClimbType { get; set; }
    public string Grade { get; set; }
    public string Technique { get; set; }
    public string Attempts { get; set; }
    public string Notes { get; set; }
    public string RockType { get; set; }
    public string PhotoPath { get; set; }

    [ObservableProperty] private string photoButtonText = "Take Photo";
    

    [RelayCommand]
    private async Task TakePhoto() {
        if (MediaPicker.Default.IsCaptureSupported) {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            photoButtonText = "Take New Photo";

            if (photo != null) {
                //Save the file into storage
                PhotoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(PhotoPath);
                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }

    [RelayCommand]
    private async Task SaveRoute() {
        RoutesDatabase database = new();
        Route route = new(SendName, ClimbType, Grade, Technique, Attempts, Notes, RockType, PhotoPath);
        await database.SaveRouteAsync(route);
        if (route.SendName == null || route.SendName == String.Empty) {
            route.SendName = $"Climb {route.Id}";
            await database.SaveRouteAsync(route);
        }

        await Shell.Current.GoToAsync("..");
    }
}