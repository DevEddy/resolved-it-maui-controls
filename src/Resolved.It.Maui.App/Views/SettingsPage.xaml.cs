using Resolved.It.Maui.App.ViewModels;

namespace Resolved.It.Maui.App.Views;

public partial class SettingsPage 
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		BindingContext = settingsPageViewModel;
		InitializeComponent();
	}
}
