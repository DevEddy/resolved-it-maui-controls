using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Resolved.It.Maui.Core.Validations;

public class ValidatableValue<T> : ObservableObject, IValidatableValue
{
    private IEnumerable<string> _errors;
    private bool _isValid;
    private T _value = default!;

    public List<IValidationRule<T>> Validations { get; } = new();

    public IEnumerable<string> Errors
    {
        get => _errors;
        private set => SetProperty(ref _errors, value);
    }

    public bool IsValid
    {
        get => _isValid;
        private set => SetProperty(ref _isValid, value);
    }

    public T Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }
    
    public string StringValue => Value?.ToString() ?? "";

    public Action<bool>? OnValidated;
    
    public ValidatableValue()
    {
        _isValid = true;
        _errors = Enumerable.Empty<string>();
    }
    
    public void SetValue(object value)
    {
        if (value is T val)
            Value = val;
    }
    
    public bool Validate(bool fireOnValidatedEvent = true)
    {
        Errors = Validations
            .Where(v => !v.Check(Value))
            .Select(v => v.ValidationMessage ?? "")
            .ToArray();

        IsValid = !Errors.Any();

        if(fireOnValidatedEvent)
            OnValidated?.Invoke(IsValid);

        return IsValid;
    }
}