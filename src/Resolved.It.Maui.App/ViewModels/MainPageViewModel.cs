using Resolved.It.Maui.App.Services;
using Resolved.It.Maui.Core.Validations;

namespace Resolved.It.Maui.App.ViewModels;

public class MainPageViewModel : BasePageViewModel
{
    public ValidatableValue<string> Note { get; } = new();

    public MainPageViewModel(INavigationService navigationService)
        : base(navigationService)
    {

    }
}

