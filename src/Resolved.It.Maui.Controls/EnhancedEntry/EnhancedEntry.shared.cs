using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Resolved.It.Maui.Core.Interfaces;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;

public partial class EnhancedEntry : Grid, IEnhancedEntry
{
    public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
        nameof (MainContent), 
        typeof (View), 
        typeof (IEnhancedEntry), 
        propertyChanged: OnContentPropertyChanged);
    private static void OnContentPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not EnhancedEntry enhancedEntry)
            return;
        
        enhancedEntry.OnMainContentChanged(oldValue, newValue);
    }
    public View MainContent
    {
        get => (View) GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }
    
    private static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        propertyName: nameof(IsPassword),
        returnType: typeof(bool),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }
    
    private static readonly BindableProperty IsPasswordVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsPasswordVisible),
        returnType: typeof(bool),
        declaringType: typeof(EnhancedEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay, 
        propertyChanged: IsPasswordVisibleChanged);

    private static void IsPasswordVisibleChanged(BindableObject bindable, object oldValue, object newValue)
    {  
        if (bindable is not EnhancedEntry enhancedEntry)
            return;

        enhancedEntry.OnIsPasswordVisibleChanged(oldValue, newValue);
    }

    public bool IsPasswordVisible
    {
        get => (bool)GetValue(IsPasswordVisibleProperty);
        set => SetValue(IsPasswordVisibleProperty, value);
    }

    private readonly Image _passwordToggleImage = new();
    private readonly Grid _entryFrameContent = new();
    private readonly Frame _entryFrame = new();
    private readonly Label _placeholderLabel = new();
    private readonly Label _errorLabel = new();
    
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

        Margin = DeviceInfo.Platform == DevicePlatform.MacCatalyst
            ? new Thickness(0, 6)
            : new Thickness(0, 4);

        _entryFrame.Padding = DeviceInfo.Platform == DevicePlatform.WinUI
            ? new Thickness(0, 0, 0, 0)
            : new Thickness(10,0);

        _entryFrameContent.RowDefinitions = new RowDefinitionCollection
        {
            new() { Height = GridLength.Star },
            new() { Height = GridLength.Auto }
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
        _placeholderLabel.VerticalOptions = LayoutOptions.Start;
        
        _errorLabel.FontSize = 11;
        _errorLabel.Margin = new Thickness(10,0);
        _errorLabel.VerticalOptions = LayoutOptions.Start;
        _errorLabel.VerticalTextAlignment = TextAlignment.Start;
    }

    private void PasswordToggleImageTapGestureOnTapped(object? sender, TappedEventArgs? e)
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    private void OnIsPasswordVisibleChanged(object oldValue, object newValue)
    {
        if (newValue is not bool isPasswordVisible)
            return;
        
        _passwordToggleImage.BackgroundColor = isPasswordVisible ? Colors.Red : Colors.Green;
    }

    private void SetupView()
    {
        Grid.SetRow(_entryFrame, 0);
        Grid.SetColumn(_passwordToggleImage, 1);

        Grid.SetRow(_placeholderLabel, 0);
        Grid.SetRow(_errorLabel, 1);
        
        _passwordToggleImage.SetBinding(IsVisibleProperty, new Binding(nameof(IsPassword), source: this));

        var passwordToggleImageTapGesture = new TapGestureRecognizer();
        passwordToggleImageTapGesture.Tapped += PasswordToggleImageTapGestureOnTapped;
        _passwordToggleImage.GestureRecognizers.Add(passwordToggleImageTapGesture);

        _entryFrameContent.Children.Add(_passwordToggleImage);
        _entryFrame.Content = _entryFrameContent;
        
        Children.Add(_entryFrame);
        Children.Add(_placeholderLabel);
        Children.Add(_errorLabel);
    }

    private void OnMainContentChanged(object oldValue, object newValue)
    {
        if (oldValue is View oldView)
        {
            oldView.HandlerChanging -= NewViewOnHandlerChanging;
            oldView.HandlerChanged -= NewViewOnHandlerChanged;
            // switch (oldValue)
            // {
            //     case Entry oldEntry:
            //         oldEntry.TextChanged -= txtEntry_TextChanged;
            //         oldEntry.Focused -= txtEntry_Focused;
            //         oldEntry.Unfocused -= txtEntry_Unfocused;
            //         break;
            //     case Picker borderlessPicker:
            //         borderlessPicker.SelectedIndexChanged -= BorderlessPicker_OnSelectedIndexChanged;
            //         break;
            //     case Editor borderlessEditor:
            //         borderlessEditor.TextChanged -= txtEntry_TextChanged;
            //         borderlessEditor.Focused -= txtEntry_Focused;
            //         borderlessEditor.Unfocused -= txtEntry_Unfocused;
            //         break;
            //     case DatePicker datePicker:
            //         datePicker.Focused -= txtEntry_Focused;
            //         datePicker.Unfocused -= txtEntry_Unfocused;
            //         datePicker.DateSelected -= DatePicker_DateSelected;
            //         break;
            // }
            _entryFrameContent.Children.Remove(oldView);
        }

        if (newValue is not View newView) 
            return;

        // if (newView is Entry newEntry)
        // {
        //     newEntry.TextChanged += txtEntry_TextChanged;
        //     newEntry.Focused += txtEntry_Focused;
        //     newEntry.Unfocused += txtEntry_Unfocused;
        // }
        // else if (newView is Picker borderlessPicker)
        // {
        //     borderlessPicker.SelectedIndexChanged += BorderlessPicker_OnSelectedIndexChanged;
        // }
        // else if (newView is Editor borderlessEditor)
        // {
        //     borderlessEditor.TextChanged += txtEntry_TextChanged;
        //     borderlessEditor.Focused += txtEntry_Focused;
        //     borderlessEditor.Unfocused += txtEntry_Unfocused;
        // }
        // else if (newView is DatePicker datePicker)
        // {
        //     datePicker.Focused += txtEntry_Focused;
        //     datePicker.Unfocused += txtEntry_Unfocused;
        //     datePicker.DateSelected += DatePicker_DateSelected;
        // }

        newView.HandlerChanging += NewViewOnHandlerChanging;
        newView.HandlerChanged += NewViewOnHandlerChanged;
        
        Grid.SetColumn(newView, 0);
        newView.HeightRequest = 44;
        
        _entryFrameContent.Children.Insert(0, newView);
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
    }    
}