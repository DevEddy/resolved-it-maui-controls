﻿<img src="https://github.com/DevEddy/resolved-it-maui-controls/blob/main/build/nuget.png" alt="Resolved IT + MAUI Logo" width=106 />

[![Build Status](https://dev.azure.com/meduardschaefer/Resolved%20IT%20-%20MAUI%20Controls/_apis/build/status/DevEddy.resolved-it-maui-controls?branchName=main)](https://dev.azure.com/meduardschaefer/Resolved%20IT%20-%20MAUI%20Controls/_build/latest?definitionId=3&branchName=main) [![NuGet](https://buildstats.info/nuget/Resolved.It.Maui.Controls?includePreReleases=true)](https://www.nuget.org/packages/Resolved.It.Maui.Controls/)

# Resolved IT - .NET MAUI Controls
Fully customizable and validatable controls and tools for MAUI.

<img src="art/preview.png#1" alt="Resolved IT Controls Preview" width=800 />

## Getting Started
In order to use the Resolved - IT .NET MAUI Controls you need to call the extension method in your `MauiProgram.cs` file as follows:

```csharp
using Resolved.It.Maui.Controls;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			// Initialize the Resolved IT - .NET MAUI Controls by adding the below line of code
			.UseResolvedItMauiControls()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		// Continue initializing your .NET MAUI App here
		return builder.Build();
	}
}
```
Define the Validatable value in your ViewModel as follows:

```csharp

public ValidatableValue<string> Email { get; } = new();

Email.Validations.Add(new EmailRule<string>
{
    ValidationMessage = "E-Mail is not valid"
});
```

Define the XAML enhanced entry as follows:
```xaml
xmlns:resolvedControls="clr-namespace:Resolved.It.Maui.Controls;assembly=Resolved.It.Maui.Controls"

<resolvedControls:EnhancedEntry 
    Placeholder="E-Mail"
    ValidatableObject="{Binding Email}">
    <resolvedControls:EnhancedEntry.MainContent>
        <Entry Keyboard="Email" IsSpellCheckEnabled="False" />
    </resolvedControls:EnhancedEntry.MainContent>
</resolvedControls:EnhancedEntry>
```

Style as you like it:
```xaml
<ContentPage.Resources>
    <ResourceDictionary>
        <Style TargetType="resolvedControls:EnhancedEntry">
            <Setter Property="OutlineColor" 
                    Value="{AppThemeBinding Light={StaticResource Black}, Dark={StaticResource White}}" />
            <Setter Property="FocusedOutlineColor" 
                    Value="{AppThemeBinding Light={StaticResource Primary}, Dark={StaticResource Primary}}" />
            <Setter Property="PlaceholderColor" 
                    Value="{AppThemeBinding Light={StaticResource Gray900}, Dark={StaticResource Gray200}}" />
            <Setter Property="PlaceholderFocusedColor" 
                    Value="{AppThemeBinding Light={StaticResource Gray900}, Dark={StaticResource Gray200}}" />
            <Setter Property="PlaceholderBackgroundColor" 
                    Value="{AppThemeBinding Light={StaticResource White}, Dark={StaticResource Black}}" />
        </Style>
    </ResourceDictionary>
</ContentPage.Resources>
```

See [Resolved.It.Maui.App](https://github.com/DevEddy/resolved-it-maui-controls/tree/main/src/Resolved.It.Maui.App) as an example.

## Features
### Enhanced Entry
- Entry, Picker, Editor and Date Picker

### Material Icons
Use the Material Icons in your App.

```xaml
xmlns:icons="clr-namespace:Resolved.It.Maui.Controls.Icons;assembly=Resolved.It.Maui.Controls"

<FontImageSource
    x:Key="LogoutIconImageSource"
    FontAutoScalingEnabled="True"
    Size="25"
    Color="{StaticResource White}"
    FontFamily="{StaticResource MaterialIconsRegular}"
    Glyph="{x:Static icons:Material.Logout}" />
    
<ToolbarItem 
    Text="" 
    IconImageSource="{StaticResource LogoutIconImageSource}" 
    Command="{Binding LogoutCommand}" />
```

## Supported platforms
All platforms supported by .NET MAUI are supported by this library. 

- Android
- iOS
- MacOS
- Windows
- Tizen: I never tried tizen and not included in the project. If anyone is interested, let me know.

## Roadmap
- Theme switcher for system, light and dark mode