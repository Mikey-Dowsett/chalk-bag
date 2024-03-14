using CommunityToolkit.Maui.Converters;
using SendTracker.Data;
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
        Date.Text = $"{route.Date}";

        if (route.Attempts == null)
            AttemptsParent.IsVisible = false;
        else
            Attempts.Text = $"{route.Attempts}";

        if (route.Technique == null)
            TechniqueParent.IsVisible = false;
        else
            Technique.Text = $"{route.Technique}";

        if (route.RockType == null)
            RockTypeParent.IsVisible = false;
        else
            RockType.Text = $"{route.RockType}";

        if (route.Duration == null)
            DurationParent.IsVisible = false;
        else
            Duration.Text = $"{route.Duration}";

        if (route.Pitches == 0)
            PitchesParent.IsVisible = false;
        else
            Pitches.Text = $"{route.Pitches}";

        if (route.Falls == 0)
            FallsParent.IsVisible = false;
        else
            Falls.Text = $"{route.Falls}";

        if (route.Rests == 0)
            RestsParent.IsVisible = false;
        else
            Rests.Text = $"{route.Rests}";

        if (route.Notes == null)
            Notes.IsVisible = false;
        else
            Notes.Text = $"{route.Notes}";

        if (route.PhotoPath == null) {
            Photo.IsVisible = false;
        }
        else {
            SupabaseSessionHandler sessionHandler = new();
            MemoryStream stream = await sessionHandler.DownloadPhoto(route.PhotoPath);
            Photo.Source = ImageSource.FromStream(() => stream);
        }
    }
}