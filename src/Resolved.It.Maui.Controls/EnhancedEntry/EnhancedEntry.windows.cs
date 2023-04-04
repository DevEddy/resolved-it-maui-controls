using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;

public partial class EnhancedEntry
{
    private static void RemoveBorder(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.TextBox textBox)
            return;

        // https://stackoverflow.com/questions/73998049/net-maui-override-the-default-style-of-a-windows-view-textbox
        textBox.Style = new Style
        {
            TargetType = typeof(Microsoft.UI.Xaml.Controls.TextBox),
            Setters =
            {
                new Setter(Microsoft.UI.Xaml.Controls.Control.FontWeightProperty, Microsoft.UI.Text.FontWeights.Thin),
                new Setter(Microsoft.UI.Xaml.Controls.Control.BorderThicknessProperty, new Microsoft.UI.Xaml.Thickness(0)),
                new Setter(Microsoft.UI.Xaml.Controls.Control.BorderBrushProperty, Colors.Transparent.ToPlatform()),
                new Setter(Microsoft.UI.Xaml.Controls.Control.BackgroundProperty, Colors.Transparent.ToPlatform())
            }
        };
    }

    private static void AddEvents(IElementHandler? handler)
    {
    }
    
    private static void RemoveEvents(IElementHandler? handler)
    {
    }

    private static void FillWidth(IElementHandler? handler)
    {
        if (handler?.PlatformView is not FrameworkElement frameworkElement)
            return;

        frameworkElement.HorizontalAlignment = HorizontalAlignment.Stretch;
    }
    
    private static void OpenDropdown(IElementHandler? handler)
    {
        if (handler?.PlatformView is not Microsoft.UI.Xaml.Controls.ComboBox comboBox)
            return;

        comboBox.IsDropDownOpen = true;
    }
}