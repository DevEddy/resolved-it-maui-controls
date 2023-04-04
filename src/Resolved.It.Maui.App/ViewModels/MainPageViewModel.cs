using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Resolved.It.Maui.App.Models;
using Resolved.It.Maui.App.Services;
using Resolved.It.Maui.App.Validations;
using Resolved.It.Maui.App.Views;
using Resolved.It.Maui.Core.Validations;

namespace Resolved.It.Maui.App.ViewModels;

public partial class MainPageViewModel : BasePageViewModel
{
    public ObservableCollection<Country> Countries { get; } = new(Country.GetCountryList());
    public ValidatableValue<string> Note { get; } = new();
    public ValidatableValue<Country> SelectedCountry { get; } = new();
    public ValidatableValue<string> Name { get; } = new();
    public ValidatableValue<string> Description { get; } = new();
    public ValidatableValue<DateTime> Timestamp { get; } = new();

    public MainPageViewModel(INavigationService navigationService)
        : base(navigationService)
    {
        SelectedCountry.Validations.Add(new IsValidCountryRule<Country> { ValidationMessage = "Land is required." });
        Timestamp.Value = DateTime.Now;
    }
    
    [RelayCommand]
    private async Task Logout()
    {
        await IsBusyFor(
            async () =>
            {
                await Task.Delay(100);
                await NavigationService.NavigateToAsync(
                    $"//{nameof(LoginPage)}",
                    new Dictionary<string, object> { { "Logout", true } });
            });
    }
}

