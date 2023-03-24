using System.Collections.Generic;

namespace Resolved.It.Maui.Core.Validations;

public interface IValidatableValue
{
    bool IsValid { get; }
    bool Validate(bool fireOnValidatedEvent = true);
    string StringValue { get; }
    IEnumerable<string> Errors { get; }
    void SetValue(object value);
}