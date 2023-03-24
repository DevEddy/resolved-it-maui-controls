using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Resolved.It.Maui.Core.Validations;
using Resolved.It.Maui.Core.Validations.Rules;

namespace Resolved.It.Maui.App;

public partial class MainPageViewModel : BasePageViewModel
{    
    [ObservableProperty]
    private bool _isValid;
    public ValidatableValue<string> Email { get; } = new();
    public ValidatableValue<string> Password { get; } = new();

    public MainPageViewModel()
    {
        AddValidations();
    }
    private bool IsInputValid() => IsValid;
    
    [RelayCommand(CanExecute = nameof(IsInputValid))]
    private async Task SignInAsync()
    {
        await IsBusyFor(
            async () =>
            {
                await Task.Delay(10);
                IsValid = true;
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
            Password.Validate(false);
            CheckValidation();
        };
        
        Password.OnValidated = _ =>
        {
            Email.Validate(false);
            CheckValidation();
        };
    }
}