using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Resolved.It.Maui.Controls.Converters;

internal class BoolToVisibilityGlyphConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? Icons.Material.Visibility : Icons.Material.Visibility_off;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() == Icons.Material.Visibility;
    }
}