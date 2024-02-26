using SendTracker.Models;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class RoutePage : ContentPage {
    private RoutePageViewModel vm;
    public RoutePage(RoutePageViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }
    
    protected override async void OnAppearing() {
        Route route = await vm.LoadData();
        Title = route.SendName;
        SendName.Text = $"{route.SendName}";
        ClimbType.Text = $"{route.ClimbType}";
        Grade.Text = $"{route.Grade}";
        Attempts.Text = $"{route.Attempts}";
        Technique.Text = $"{route.Technique}";
        RockType.Text = $"{route.RockType}";

        if (route.Notes == null) {
            NotesFrame.IsVisible = false;
        }
        else {
            Notes.Text = $"{route.Notes}";
        }
        
        if (route.PhotoPath == null) {
            PhotoFrame.IsVisible = false;
        }
        else {
            Photo.Source = new FileImageSource();
            Photo.Source = ImageSource.FromFile(route.PhotoPath);
        }
    }
}