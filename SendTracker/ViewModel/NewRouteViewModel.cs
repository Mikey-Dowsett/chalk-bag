using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;

namespace SendTracker.ViewModel;

[QueryProperty(nameof(RouteId), "Id")]
public partial class NewRouteViewModel : ObservableObject, INotifyPropertyChanged {
    private string duration;

    private int falls;

    private string mediaText = "Add Media";

    private bool optionsVisible;

    private bool proposed;

    private int rests;
    public int RouteId { get; set; }

    public string? SendName { get; set; }
    public string? ClimbType { get; set; }
    public string? Grade { get; set; }
    public string? Technique { get; set; }
    public string? Attempts { get; set; }
    public string? Notes { get; set; }
    public string? RockType { get; set; }
    public string? PhotoPath { get; set; }

    public string? Duration {
        get => duration;
        set {
            duration = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Duration)));
        }
    }

    public bool Proposed {
        get => proposed;
        set {
            proposed = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Proposed)));
        }
    }

    public ICommand OptionsCommand => new Command(ShowOptions);

    public bool OptionsVisible {
        get => optionsVisible;
        set {
            if (optionsVisible == value) return;
            optionsVisible = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OptionsVisible)));
        }
    }

    public string MediaText {
        get => mediaText;
        set {
            if (mediaText == value) return;
            mediaText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MediaText)));
        }
    }

    private int pitches;
    public int Pitches {
        get => pitches;
        set {
            if (pitches == value) return;
            pitches = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pitches)));
        }
    }

    public int Falls {
        get => falls;
        set {
            if (falls == value) return;
            falls = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Falls)));
        }
    }

    public int Rests {
        get => rests;
        set {
            if (rests == value) return;
            rests = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rests)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

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
        if (SendName == null || Notes == null || PhotoPath == null)
            result = await Shell.Current.DisplayAlert("Continue?",
                errorMessage, "Yes", "No");

        if (result) {
            RoutesDatabase database = new();
            if (Proposed) Grade += "*";
            Route route = null;
            if (RouteId == 0) {
                route = new Route(SendName, ClimbType, Grade, Technique, Attempts, Notes, RockType, PhotoPath,
                    DateTime.Now, Duration, Pitches, Proposed, rests, falls);
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
                route.Falls = Falls;
                route.Rests = Rests;
                route.LoadEmoji();
            }

            await database.SaveRouteAsync(route);
            if (route.SendName == null || route.SendName == string.Empty) {
                route.SendName = $"Climb {route.Id}";
                await database.SaveRouteAsync(route);
            }
            
            CancellationTokenSource cancellationToken = new();
            IToast toast = Toast.Make("Route Saved!", ToastDuration.Short, 25);
            await toast.Show(cancellationToken.Token);

            await Shell.Current.GoToAsync("..");
        }
    }
}