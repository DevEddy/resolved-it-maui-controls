using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resolved.It.Maui.App.Services;
using Resolved.It.Maui.App.Views;
using Resolved.It.Maui.Core.Validations;
using Resolved.It.Maui.Core.Validations.Rules;

namespace Resolved.It.Maui.App.ViewModels;

public partial class LoginPageViewModel : BasePageViewModel
{    
    [ObservableProperty]
    private bool _isValid;
    public ValidatableValue<string> Email { get; } = new();
    public ValidatableValue<string> Password { get; } = new();

    private readonly ISettingsService _settingsService;

    public LoginPageViewModel(
        INavigationService navigationService,
        ISettingsService settingsService)
        : base(navigationService)
    {
        _settingsService = settingsService;

        AddValidations();

        Email.Value = "mail@me.de";
    }
    
    private bool IsInputValid() => IsValid;
    
    [RelayCommand(CanExecute = nameof(IsInputValid))]
    private async Task SignInAsync()
    {
        await IsBusyFor(
            async () =>
            {
                await Task.Delay(2000);
                _settingsService.AuthAccessToken = "MOCK";

                await NavigationService.NavigateToAsync($"//{nameof(MainPage)}");
            });
    }
    
    private void CheckValidation()
    {
        var isEmailValid = Email.IsValid;
        var isPasswordValid = Password.IsValid;

        IsValid = isEmailValid && isPasswordValid;

        SignInCommand.NotifyCanExecuteChanged();
    }

    private void AddValidations()
    {
        Email.Validations.Add(new EmailRule<string>
        {
            ValidationMessage = "E-Mail is not valid"
        });
        
        Password.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Password is required"
        });
        
        Email.OnValidated = _ =>
        {
            CheckValidation();
        };
        
        Password.OnValidated = _ =>
        {
            CheckValidation();
        };
    }
}