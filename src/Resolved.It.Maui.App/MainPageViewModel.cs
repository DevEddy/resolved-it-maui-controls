using Resolved.It.Maui.Core.Validations;
using Resolved.It.Maui.Core.Validations.Rules;

namespace Resolved.It.Maui.App;

public class MainPageViewModel
{
    public ValidatableValue<string> Email { get; } = new();
    public ValidatableValue<string> Password { get; } = new();

    public MainPageViewModel()
    {
        Email.Validations.Add(new EmailRule<string>
        {
            ValidationMessage = "E-Mail is not valid"
        });
        
        Password.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Password is required"
        });
    }
}