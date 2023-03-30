using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;

public partial class EnhancedEntry
{
    private static void RemoveBorder(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.TextBox textBox)
            return;
        
        textBox.FontWeight = Microsoft.UI.Text.FontWeights.Thin;
        textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
        textBox.BorderBrush = Colors.Transparent.ToPlatform();
        textBox.Background = Colors.Transparent.ToPlatform();
    }

    private static void AddEvents(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.TextBox textBox)
            return;
        
        textBox.GettingFocus += TextBox_GettingFocus;
    }
    
    private static void RemoveEvents(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.TextBox textBox)
            return;
        
        textBox.GettingFocus -= TextBox_GettingFocus;
    }

    private static void TextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
    {
        if (sender is not Microsoft.UI.Xaml.Controls.TextBox textBox)
            return;
        
        textBox.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
        textBox.BorderBrush = Colors.Transparent.ToPlatform();
        textBox.Background = Colors.Transparent.ToPlatform();
        textBox.FocusVisualPrimaryThickness = textBox.BorderThickness; 
        textBox.FocusVisualSecondaryThickness = textBox.BorderThickness; 
        textBox.SelectionHighlightColor = new SolidColorBrush(Colors.Transparent.ToWindowsColor());
        
        textBox.Resources.Remove("TextControlBorderThemeThicknessFocused");
        textBox.Resources.Remove("TextControlBorderThemeThickness");
        textBox.Resources["TextControlBorderThemeThicknessFocused"] = textBox.BorderThickness;
        textBox.Resources["TextControlBorderThemeThickness"] = textBox.BorderThickness;
    }

    private static void FillWidth(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.ComboBox comboBox)
            return;

        comboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
    }
    
    private static void OpenDropdown(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.ComboBox comboBox)
            return;

        comboBox.IsDropDownOpen = true;
    }
}