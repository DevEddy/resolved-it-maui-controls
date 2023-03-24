<img src="https://github.com/DevEddy/resolved-it-maui-controls/blob/main/build/nuget.png" alt="Resolved IT + MAUI Logo" width=106 />

# Resolved IT - MAUI Controls
Fully customizable and validatable controls for MAUI.

## Getting Started
In order to use the Resolved -IT .NET MAUI Controls you need to call the extension method in your `MauiProgram.cs` file as follows:

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

```csharp
Define the Validatable value in your ViewModel as follows:

public ValidatableValue<string> Email { get; } = new();

Email.Validations.Add(new EmailRule<string>
{
    ValidationMessage = "E-Mail is not valid"
});
```

Define the XAML enhanced entry as follows:
```csharp
xmlns:resolvedControls="clr-namespace:Resolved.It.Maui.Controls;assembly=Resolved.It.Maui.Controls"

<resolvedControls:EnhancedEntry 
    Placeholder="E-Mail"
    ValidatableObject="{Binding Email}">
    <resolvedControls:EnhancedEntry.MainContent>
        <Entry Text="{Binding Email.Value}" Keyboard="Email" IsSpellCheckEnabled="False" />
    </resolvedControls:EnhancedEntry.MainContent>
</resolvedControls:EnhancedEntry>
```

Style as you like it:
```csharp
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

## Supported input types
- Entry

## Roadmap
- Support for entry types *Editor*, *Picker* and *DatePicker*
- Icons support for password toggle button

## Screenshots
<img src="https://github.com/DevEddy/resolved-it-maui-controls/blob/main/art/android_screenshot.png" alt="Resolved IT + MAUI Logo" width=300 />
