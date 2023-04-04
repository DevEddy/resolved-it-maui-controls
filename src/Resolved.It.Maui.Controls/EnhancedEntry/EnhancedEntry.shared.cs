using System;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Resolved.It.Maui.Controls.Converters;
using Resolved.It.Maui.Core.Validations;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;

public partial class EnhancedEntry : Grid
{
    #region Bindable Properties

    public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
        propertyName: nameof (MainContent),
        returnType: typeof(View),
        declaringType: typeof(EnhancedEntry), 
        propertyChanged: OnContentPropertyChanged);
        
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        propertyName: nameof(IsPassword),
        returnType: typeof(bool),
        declaringType: typeof(EnhancedEntry),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty EnablePasswordToggleProperty = BindableProperty.Create(
        propertyName: nameof(EnablePasswordToggle),
        returnType: typeof(bool),
        declaringType: typeof(EnhancedEntry),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
        propertyName: nameof(PlaceholderColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.Gray,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderFocusedColorProperty = BindableProperty.Create(
        propertyName: nameof(PlaceholderFocusedColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.Black,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderBackgroundColorProperty = BindableProperty.Create(
        propertyName: nameof(PlaceholderBackgroundColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.White,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderErrorColorProperty = BindableProperty.Create(
        propertyName: nameof(PlaceholderErrorColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.OrangeRed,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty OutlineColorProperty = BindableProperty.Create(
        propertyName: nameof(OutlineColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.Black,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty FocusedOutlineColorProperty = BindableProperty.Create(
        propertyName: nameof(FocusedOutlineColor),
        returnType: typeof(Color),
        declaringType: typeof(EnhancedEntry),
        defaultValue: Colors.Black,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty ValidatableObjectProperty = BindableProperty.Create(
        propertyName: nameof(ValidatableObject),
        returnType: typeof(IValidatableValue),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: ValidatableObjectChanged);

    private static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        propertyName: nameof(ErrorText),
        returnType: typeof(string),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    private static readonly BindableProperty HasErrorProperty = BindableProperty.Create(
        propertyName: nameof(HasError),
        returnType: typeof(bool),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public View MainContent
    {
        get => (View) GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }
    
    public IValidatableValue? ValidatableObject
    {
        get => (IValidatableValue?)GetValue(ValidatableObjectProperty);
        set => SetValue(ValidatableObjectProperty, value);
    }
    
    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }    
    
    public bool EnablePasswordToggle
    {
        get => (bool)GetValue(EnablePasswordToggleProperty);
        set => SetValue(EnablePasswordToggleProperty, value);
    }
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    
    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }
    
    public Color PlaceholderFocusedColor
    {
        get => (Color)GetValue(PlaceholderFocusedColorProperty);
        set => SetValue(PlaceholderFocusedColorProperty, value);
    }    
    
    public Color PlaceholderBackgroundColor
    {
        get => (Color)GetValue(PlaceholderBackgroundColorProperty);
        set => SetValue(PlaceholderBackgroundColorProperty, value);
    }    

    public Color PlaceholderErrorColor
    {
        get => (Color)GetValue(PlaceholderErrorColorProperty);
        set => SetValue(PlaceholderErrorColorProperty, value);
    } 

    public Color OutlineColor
    {
        get => (Color)GetValue(OutlineColorProperty);
        set => SetValue(OutlineColorProperty, value);
    }
    
    public Color FocusedOutlineColor
    {
        get => (Color)GetValue(FocusedOutlineColorProperty);
        set => SetValue(FocusedOutlineColorProperty, value);
    }
    
    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }
    
    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    #endregion Bindable Properties

    private const double DefaultHeight = 44;

    private readonly Image _passwordToggleImage = new();
    private readonly Grid _entryFrameContent = new();
    private readonly Frame _entryFrame = new();
    private readonly Label _placeholderLabel = new();
    private readonly Label _errorLabel = new();
        
    private View? _mainEntryControl;
    private double _currentEditorHeight;
    private double HeightForPlaceholderCalculation => _currentEditorHeight > 0 ? _currentEditorHeight : DefaultHeight; 

    public EnhancedEntry()
    {
        SetupStyle();
        SetupView();
    }
    
    private void SetupStyle()
    {
        RowDefinitions = new RowDefinitionCollection()
        {
            new() { Height = GridLength.Auto },
            new() { Height = GridLength.Auto }
        };

        Margin = 0;
        Padding = 0;

        _entryFrame.BackgroundColor = Colors.Transparent;
        _entryFrame.Padding = DeviceInfo.Platform == DevicePlatform.WinUI
            ? new Thickness(0, 0, 0, 0)
            : new Thickness(10,0);

        _entryFrame.SetBinding(Microsoft.Maui.Controls.Frame.BorderColorProperty, new Binding(nameof(OutlineColor), source: this));

        _entryFrameContent.ColumnDefinitions = new ColumnDefinitionCollection()
        {
            new() { Width = GridLength.Star },
            new() { Width = GridLength.Auto }
        };
        
        _passwordToggleImage.WidthRequest = 21;
        _passwordToggleImage.HeightRequest = 21;
        _passwordToggleImage.Margin = DeviceInfo.Platform == DevicePlatform.WinUI
            ? new Thickness(0, 0, 10, 0)
            : new Thickness(0);
        
        _placeholderLabel.FontSize = 15;
        _placeholderLabel.Margin = new Thickness(10,0);
        _placeholderLabel.Padding = new Thickness(4, 0);
        _placeholderLabel.HorizontalOptions = LayoutOptions.Start;
        _placeholderLabel.VerticalOptions = LayoutOptions.Center;
        _placeholderLabel.SetBinding(Label.BackgroundColorProperty, new Binding(nameof(PlaceholderBackgroundColor), source: this));
        _placeholderLabel.SetBinding(Label.TextProperty, new Binding(nameof(PlaceholderColor), source: this));

        _errorLabel.FontSize = 11;
        _errorLabel.Margin = new Thickness(10,0);
        _errorLabel.VerticalOptions = LayoutOptions.Start;
        _errorLabel.VerticalTextAlignment = TextAlignment.Center;
        _errorLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorText), source: this));
        _errorLabel.SetBinding(IsVisibleProperty, new Binding(nameof(HasError), source: this));
        _errorLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(_placeholderLabel.TextColor), source: _placeholderLabel));
    }

    private void PasswordToggleImageTapGestureOnTapped(object? sender, TappedEventArgs? e)
    {
        IsPassword = !IsPassword;
        _mainEntryControl?.Focus();
    }

    private void SetupView()
    {
        Grid.SetRow(_entryFrame, 0);
        Grid.SetColumn(_passwordToggleImage, 1);

        Grid.SetRow(_placeholderLabel, 0);
        Grid.SetRow(_errorLabel, 1);

        var passwordToggleImageSource = new FontImageSource
        {
            FontFamily = "MaterialIconsRegular",
            Color = FocusedOutlineColor,
            Size = 80,
            FontAutoScalingEnabled = true,
            Glyph = Icons.Material.Visibility_off
        };
        passwordToggleImageSource.SetBinding(FontImageSource.ColorProperty, new Binding(nameof(FocusedOutlineColor), source: this));
        passwordToggleImageSource.SetBinding(FontImageSource.GlyphProperty, new Binding(nameof(IsPassword), source: this, converter: new BoolToVisibilityGlyphConverter()));

        var passwordToggleImageTapGesture = new TapGestureRecognizer();
        passwordToggleImageTapGesture.Tapped += PasswordToggleImageTapGestureOnTapped;
        _passwordToggleImage.GestureRecognizers.Add(passwordToggleImageTapGesture);
        _passwordToggleImage.SetBinding(IsVisibleProperty, new Binding(nameof(EnablePasswordToggle), source: this, mode: BindingMode.TwoWay));
        _passwordToggleImage.Source = passwordToggleImageSource;
        
        _entryFrameContent.Children.Add(_passwordToggleImage);
        
        _entryFrame.Content = _entryFrameContent;
        
        var placeholderTapGesture = new TapGestureRecognizer();
        placeholderTapGesture.Tapped += PlaceholderTapGestureOnTapped;
        _placeholderLabel.GestureRecognizers.Add(placeholderTapGesture);
        
        _placeholderLabel.SetBinding(Label.TextProperty, new Binding(nameof(Placeholder), source: this));
        
        Children.Add(_entryFrame);
        Children.Add(_placeholderLabel);
        Children.Add(_errorLabel);
    }

    private static void ValidatableObjectChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not EnhancedEntry enhancedEntry)
            return;
        
        enhancedEntry.OnValidatableObjectChanged(newValue);
    }

    private void OnValidatableObjectChanged(object newValue)
    {
        switch (_mainEntryControl)
        {
            case Entry entry:
                entry.SetBinding(Entry.TextProperty, new Binding("Value", source: newValue, mode: BindingMode.TwoWay));
                break;
            case Editor editor:
                editor.SetBinding(Editor.TextProperty, new Binding("Value", source: newValue, mode: BindingMode.TwoWay));
                break;
            case DatePicker datePicker:
                datePicker.SetBinding(DatePicker.DateProperty, new Binding("Value", source: newValue, mode: BindingMode.TwoWay));
                break;
        }

        Validate();
    }

    private static void OnContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not EnhancedEntry enhancedEntry)
            return;
        
        enhancedEntry.OnMainContentChanged(oldValue, newValue);
    }
    
    private void OnMainContentChanged(object oldValue, object newValue)
    {
        if (oldValue is View oldView)
        {
            oldView.HandlerChanging -= NewViewOnHandlerChanging;
            oldView.HandlerChanged -= NewViewOnHandlerChanged;
            
            switch (oldValue)
            {
                case Entry oldEntry:
                    oldEntry.TextChanged -= Handle_EntryValueChanged;
                    oldEntry.Focused -= Handle_EntryFocused;
                    oldEntry.Unfocused -= Handle_EntryUnfocused;
                    oldEntry.RemoveBinding(Entry.IsPasswordProperty);
                    oldEntry.RemoveBinding(Entry.TextProperty);
                    break;
                case Editor oldEditor:
                    oldEditor.TextChanged -= Handle_EntryValueChanged;
                    oldEditor.Focused -= Handle_EntryFocused;
                    oldEditor.Unfocused -= Handle_EntryUnfocused;
                    oldEditor.SizeChanged -= Handle_EditorSizeChanged;
                    oldEditor.RemoveBinding(Editor.TextProperty);
                    break;
                case Picker oldPicker:
                    oldPicker.Focused -= Handle_EntryFocused;
                    oldPicker.Unfocused -= Handle_EntryUnfocused;
                    oldPicker.SelectedIndexChanged -= Handle_PickerSelectedIndexChanged;
                    break;
                case DatePicker oldDatePicker:
                    oldDatePicker.Focused -= Handle_EntryFocused;
                    oldDatePicker.Unfocused -= Handle_EntryUnfocused;
                    oldDatePicker.DateSelected -= Handle_DatePickerDateSelected;
                    break;
            }
            _entryFrameContent.Children.Remove(oldView);
        }

        if (newValue is not View newView) 
            return;

        _mainEntryControl = newView;
        
        switch (_mainEntryControl)
        {
            case Entry newEntry:
                newEntry.TextChanged += Handle_EntryValueChanged;
                newEntry.Focused += Handle_EntryFocused;
                newEntry.Unfocused += Handle_EntryUnfocused;
                newEntry.RemoveBinding(Entry.PlaceholderProperty);
                newEntry.RemoveBinding(Entry.TextProperty);
                newEntry.RemoveBinding(Entry.BackgroundColorProperty);
                newEntry.Placeholder = "";
                newEntry.BackgroundColor = Colors.Transparent;
                newEntry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this, mode: BindingMode.TwoWay));
                newEntry.HeightRequest = DefaultHeight;
                break;
            case Editor newEditor:
                newEditor.TextChanged += Handle_EntryValueChanged;
                newEditor.Focused += Handle_EntryFocused;
                newEditor.Unfocused += Handle_EntryUnfocused;
                newEditor.SizeChanged += Handle_EditorSizeChanged;
                newEditor.RemoveBinding(Editor.PlaceholderProperty);
                newEditor.RemoveBinding(Editor.TextProperty);
                newEditor.RemoveBinding(Editor.BackgroundColorProperty);
                newEditor.Placeholder = "";

                if(DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.WinUI)
                    newEditor.Margin = new Thickness(0, 8, 0, 0);

                newEditor.AutoSize = EditorAutoSizeOption.TextChanges;
                newEditor.BackgroundColor = Colors.Transparent;
                break;
            case Picker picker:
                picker.SelectedIndexChanged += Handle_PickerSelectedIndexChanged;
                picker.BackgroundColor = Colors.Transparent;
                picker.HeightRequest = DefaultHeight;
                break;
            case DatePicker datePicker:
                datePicker.Focused += Handle_EntryFocused;
                datePicker.Unfocused += Handle_EntryUnfocused;
                datePicker.DateSelected += Handle_DatePickerDateSelected;
                datePicker.BackgroundColor = Colors.Transparent;
                datePicker.HeightRequest = DefaultHeight;
                break;
            default:
                throw new NotSupportedException($"Content type {_mainEntryControl?.GetType().Name} is not supported yet.");
        }
        _mainEntryControl.HandlerChanging += NewViewOnHandlerChanging;
        _mainEntryControl.HandlerChanged += NewViewOnHandlerChanged;
        
        Grid.SetColumn(_mainEntryControl, 0);
        
        _entryFrameContent.Children.Insert(0, _mainEntryControl);
    }

    private void Handle_EditorSizeChanged(object? sender, EventArgs e)
    {
        if (sender is not Editor editor || editor.Height <= 0)
            return;

        _currentEditorHeight = editor.Height + editor.Margin.Top;
        UpdateControlsState(editor.IsFocused);
    }

    private void Handle_PickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sender is not Picker picker)
            return;

        var selectedIndex = picker.SelectedIndex;

        if (selectedIndex == -1) 
            return;

        if (picker.ItemsSource[selectedIndex] is not { } obj) 
            return;
        
        ValidatableObject?.SetValue(obj);
        Validate();
    }

    private void Handle_DatePickerDateSelected(object? sender, DateChangedEventArgs e)
    {
        Validate();
    }

    private void Handle_EntryUnfocused(object? sender, FocusEventArgs e)
    {
        UpdateControlsState(false);
    }

    private void Handle_EntryFocused(object? sender, FocusEventArgs e)
    {
        UpdateControlsState(true);
    }

    private void Handle_EntryValueChanged(object? sender, TextChangedEventArgs e)
    {
        Validate();
    }

    private static void NewViewOnHandlerChanging(object? sender, HandlerChangingEventArgs e)
    {
        if(e.OldHandler is not { } oldHandler)
            return;
        
        // we remove the previous registered native events here
        // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/handlers/customize?view=net-maui-7.0#conditional-compilation
        RemoveEvents(oldHandler);
    }

    private static void NewViewOnHandlerChanged(object? sender, EventArgs e)
    {
        if(sender is not IView view)
            return;
        
        // we register native events here
        // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/handlers/customize?view=net-maui-7.0#conditional-compilation
        AddEvents(view.Handler);
        RemoveBorder(view.Handler);
        FillWidth(view.Handler);
    }

    private void PlaceholderTapGestureOnTapped(object? sender, TappedEventArgs e)
    {
        _mainEntryControl?.Focus();
        OpenDropdown(_mainEntryControl?.Handler);
        UpdateControlsState(true);
    }

    private void UpdateControlsState(bool isFocused)
    {
        var hasValue = !string.IsNullOrWhiteSpace(ValidatableObject?.StringValue);
        
        if (HasError)
        {
            _placeholderLabel.TextColor = PlaceholderErrorColor;
            _entryFrame.BorderColor = PlaceholderErrorColor;
        }
        else if (isFocused)
        {
            _placeholderLabel.TextColor = PlaceholderFocusedColor;
            _entryFrame.BorderColor = FocusedOutlineColor;
        }
        else
        {
            _placeholderLabel.TextColor = PlaceholderColor;
            _entryFrame.BorderColor = OutlineColor;
        }

        _placeholderLabel.FontSize = GetPlaceholderFontSize();
        _placeholderLabel.TranslateTo(0, GetPlaceholderYTranslation(), 80, easing: Easing.Linear);
        
        double GetPlaceholderYTranslation()
        {
            if (isFocused || hasValue)
                return HeightForPlaceholderCalculation / 2 * -1;

            return 0;
        }
        
        double GetPlaceholderFontSize()
        {
            return isFocused || hasValue ? 11 : 14;
        }
    }

    private void Validate()
    {
        ValidatableObject?.Validate();
        
        if (ValidatableObject?.Errors.Any() ?? false)
        {
            var firstError = ValidatableObject.Errors.ElementAt(0);
            ErrorText = firstError;
            HasError = true;
        }
        else
        {
            ErrorText = "";
            HasError = false;
        }
        UpdateControlsState(_mainEntryControl?.IsFocused ?? false);
    }
}