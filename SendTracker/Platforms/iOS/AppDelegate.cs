using Foundation;

namespace SendTracker;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate {
    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}