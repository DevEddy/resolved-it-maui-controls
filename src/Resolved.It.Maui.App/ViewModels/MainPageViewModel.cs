using System.Collections.ObjectModel;
using Resolved.It.Maui.App.Models;
using Resolved.It.Maui.App.Services;
using Resolved.It.Maui.App.Validations;
using Resolved.It.Maui.Core.Validations;

namespace Resolved.It.Maui.App.ViewModels;

public class MainPageViewModel : BasePageViewModel
{
    public ObservableCollection<Country> Countries { get; } = new(Country.GetCountryList());

    public ValidatableValue<string> Note { get; } = new();
    public ValidatableValue<Country> SelectedCountry { get; } = new();
    public ValidatableValue<string> Name { get; } = new();

    public MainPageViewModel(INavigationService navigationService)
        : base(navigationService)
    {
        SelectedCountry.Validations.Add(new IsValidCountryRule<Country> { ValidationMessage = "Land is required." });
    }
}

