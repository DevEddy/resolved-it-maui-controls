using System;
using Microsoft.Maui.Hosting;
using Resolved.It.Maui.Core;

namespace Resolved.It.Maui.Controls;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseResolvedItMauiControls(this MauiAppBuilder builder, Action<Options>? options = default)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
        });

        // Pass `null` because `options?.Invoke()` will set options on both `Resolved.It.Maui.Controls` and `Resolved.It.Maui.Core`
        builder.UseResolvedItMauiCore();

        // Invokes options for both `Resolved.It.Maui.Controls` and `Resolved.It.Maui.Core`
        options?.Invoke(new Options());

        return builder;
    }
}