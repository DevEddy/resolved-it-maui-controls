using Resolved.It.Maui.App.Models;
using Resolved.It.Maui.Core.Validations;

namespace Resolved.It.Maui.App.Validations
{
    public class IsValidCountryRule<T> : IValidationRule<T>
    {
        public string? ValidationMessage { get; set; }

        public bool Check(T? value) =>
            value is Country country && !string.IsNullOrEmpty(country.Code);

    }
}

