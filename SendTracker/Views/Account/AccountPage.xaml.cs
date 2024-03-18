using SendTracker.Data;
using SendTracker.ViewModel;

namespace SendTracker.Views;

public partial class AccountPage : ContentPage {
    private readonly AccountPageViewModel vm;

    public AccountPage(AccountPageViewModel vm) {
        InitializeComponent();

        BindingContext = vm;
        this.vm = vm;
    }

    protected override async void OnAppearing() {
        await vm.DisplayCurrentRoutes();
        var user = await UserHandler.GetUser(SessionHandler.UserSession.User.Id);
        Title = user.Username;
    }
}