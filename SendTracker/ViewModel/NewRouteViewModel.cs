using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.Views;

namespace SendTracker.ViewModel;

[QueryProperty(nameof(RouteId), "Id")]
public partial class NewRouteViewModel : ObservableObject, INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    public int RouteId { get; set; }
    
    public string? SendName { get; set; }
    public string? ClimbType { get; set; }
    public string? Grade { get; set; }
    public string? Technique { get; set; }
    public string? Attempts { get; set; }
    public string? Notes { get; set; }
    public string? RockType { get; set; }
    public string? PhotoPath { get; set; }

    private string duration;
    public string? Duration {
        get => duration;
        set {
            duration = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Duration)));
        } 
    }

    private bool proposed;
    public bool Proposed {
        get => proposed;
        set {
            proposed = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Proposed)));
        } 
    }
    
    public ICommand OptionsCommand => new Command(ShowOptions);
    
    private bool optionsVisible = false;
    public bool OptionsVisible {
        get => optionsVisible;
        set {
            if (optionsVisible == value) return;
            optionsVisible = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OptionsVisible)));
        }
    }

    private string mediaText = "Add Media";

    public string MediaText {
        get => mediaText;
        set {
            if (mediaText == value) return;
            mediaText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MediaText)));
        }
    }

    private int pitches = 1;
    public int Pitches {
        get => pitches;
        set {
            if (pitches == value) return;
            pitches = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pitches)));
        }
    }

    private void ShowOptions(object obj) {
        OptionsVisible = !OptionsVisible;
    }
    
    [RelayCommand]
    private async Task TakePhoto(string mode) {
        if (MediaPicker.Default.IsCaptureSupported) {
            FileResult photo = null;
            switch (mode) {
                case "photo": 
                    photo = await MediaPicker.CapturePhotoAsync();
                    break;
                case "photos":
                    photo = await MediaPicker.PickPhotoAsync();
                    break;
                case "video":
                    photo = await MediaPicker.CaptureVideoAsync();
                    break;
                case "videos":
                    photo = await MediaPicker.PickVideoAsync();
                    break;
            }

            if (photo != null) {
                //Save the file into storage
                PhotoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(PhotoPath);
                await sourceStream.CopyToAsync(localFileStream);
            }

            MediaText = PhotoPath == null ? "Add Media" : "Media Added";
        }
    }
    
    [RelayCommand]
    public async Task SaveRoute() {
        bool result = true;
        string errorMessage = SendName == null ? "Missing Name!\n" : "";
        errorMessage += Notes == null ? "Missing Notes!\n" : "";
        errorMessage += PhotoPath == null ? "Missing Media!\n" : "";
        if (SendName == null || Notes == null || PhotoPath == null) {
            result = await Shell.Current.DisplayAlert("Continue?",
                errorMessage, "Yes", "No");
        }

        if (result == true) {
            RoutesDatabase database = new();
            if (Proposed) Grade += "*";
            Route route = null;
            if (RouteId == 0) {
                route = new(SendName, ClimbType, Grade, Technique, Attempts, Notes, RockType, PhotoPath,
                    DateTime.Now, Duration, Pitches, Proposed);
            }
            else {
                route = await database.GetRouteAsync(RouteId);
                route.SendName = SendName;
                route.ClimbType = ClimbType;
                route.Grade = Grade;
                route.Technique = Technique;
                route.Attempts = Attempts;
                route.Notes = Notes;
                route.RockType = RockType;
                route.PhotoPath = PhotoPath;
                route.Duration = Duration;
                route.Pitches = Pitches;
                route.Proposed = Proposed;
            }
            await database.SaveRouteAsync(route);
            if (route.SendName == null || route.SendName == string.Empty) {
                route.SendName = $"Climb {route.Id}";
                await database.SaveRouteAsync(route);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}