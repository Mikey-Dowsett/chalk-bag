using SendTracker.Models;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class RoutePage : ContentPage {
    private readonly RoutePageViewModel vm;

    public RoutePage(RoutePageViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }

    protected override async void OnAppearing() {
        Route route = await vm.LoadData();
        Title = route.SendName;
        ClimbType.Text = $"{route.ClimbType}";
        Grade.Text = $"{route.Grade}";
        Attempts.Text = $"{route.Attempts}";
        Technique.Text = $"{route.Technique}";
        RockType.Text = $"{route.RockType}";
        Date.Text = $"{route.Date}";

        if (route.Notes == null)
            Notes.IsVisible = false;
        else
            Notes.Text = $"{route.Notes}";

        if (route.PhotoPath == null) {
            Photo.IsVisible = false;
        }
        else {
            Photo.Source = new FileImageSource();
            Photo.Source = ImageSource.FromFile(route.PhotoPath);
        }
    }
}