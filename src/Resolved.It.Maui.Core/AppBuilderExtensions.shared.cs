using System;
using Microsoft.Maui.Hosting;

namespace Resolved.It.Maui.Core;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseResolvedItMauiCore(this MauiAppBuilder builder, Action<Options>? options = default)
    {
        options?.Invoke(new Options());
        return builder;
    }
}