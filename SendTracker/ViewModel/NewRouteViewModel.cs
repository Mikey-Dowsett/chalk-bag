using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;
using SendTracker.Models;
using SendTracker.Views;

namespace SendTracker.ViewModel;

public partial class NewRouteViewModel : ObservableObject, INotifyPropertyChanged {
    public string SendName { get; set; }
    public string ClimbType { get; set; }
    public string Grade { get; set; }
    public string Technique { get; set; }
    public string Attempts { get; set; }
    public string Notes { get; set; }
    public string RockType { get; set; }
    public string PhotoPath { get; set; }
    
    public event PropertyChangedEventHandler PropertyChanged;
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

    private bool mediaVisible = false;

    public bool MediaVisible {
        get => mediaVisible;
        set {
            if (mediaVisible == value) return;
            mediaVisible = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MediaVisible)));
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

            MediaVisible = PhotoPath != null;
        }
    }
    
    [RelayCommand]
    public async Task SaveRoute() {
        RoutesDatabase database = new();
        Route route = new(SendName, ClimbType, Grade, Technique, Attempts, Notes, RockType, PhotoPath,
            DateTime.Now);
        await database.SaveRouteAsync(route);
        if (route.SendName == null || route.SendName == string.Empty) {
            route.SendName = $"Climb {route.Id}";
            await database.SaveRouteAsync(route);
        }
    }
}