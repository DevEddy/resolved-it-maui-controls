using System;
namespace Resolved.It.Maui.App.Services;

public class SettingsService : ISettingsService
{
    public string AuthAccessToken
    {
        get => Preferences.Get(nameof(AuthAccessToken), "");
        set => Preferences.Set(nameof(AuthAccessToken), value);
    }

    public int Theme
    {
        get => Preferences.Get(nameof(Theme), 0);
        set => Preferences.Set(nameof(Theme), value);
    }
}