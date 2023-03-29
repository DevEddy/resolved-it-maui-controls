using Resolved.It.Maui.App.Views;

namespace Resolved.It.Maui.App.Services;

public class MauiNavigationService : INavigationService
{
    private readonly ISettingsService _settingsService;

    public MauiNavigationService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public Task InitializeAsync() =>
        NavigateToAsync(
            string.IsNullOrEmpty(_settingsService.AuthAccessToken)
                ? $"//{nameof(LoginPage)}"
                : $"//{nameof(MainPage)}");

    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

        return routeParameters != null
            ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
            : Shell.Current.GoToAsync(shellNavigation);
    }

    public Task PopAsync() =>
        Shell.Current.GoToAsync("..");
}

