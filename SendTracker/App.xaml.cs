using SendTracker.ViewModel;
using SendTracker.Views;

namespace SendTracker;

public partial class App : Application {
    public App() {
        InitializeComponent();

        MainPage = new AuthenticationPage(new AuthenticationPageViewModel());
    }
}