using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SendTracker.Data;

namespace SendTracker.ViewModel;

public partial class MainPageViewModel : ObservableObject {
    [ObservableProperty] private string email;
    [ObservableProperty] private string username;
    [ObservableProperty] private string password;
    [ObservableProperty] private string confirmPassword;
    
    [RelayCommand]
    private async Task Login() {
        await SupabaseSessionHandler.LoginUser(email, password);
        if(SupabaseSessionHandler.Session != null) { 
            Application.Current.MainPage = new AppShell();
        }
    }
}