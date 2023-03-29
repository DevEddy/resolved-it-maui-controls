using Resolved.It.Maui.App.Services;

namespace Resolved.It.Maui.App;

public partial class App : Application
{
    public App(INavigationService navigationService)
    {
        InitializeComponent();

        MainPage = new AppShell(navigationService);
    }
}