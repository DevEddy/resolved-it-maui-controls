using Resolved.It.Maui.App.ViewModels;

namespace Resolved.It.Maui.App.Views;

public partial class LoginPage
{
    public LoginPage(LoginPageViewModel loginPageViewModel)
    {
        BindingContext = loginPageViewModel;
        InitializeComponent();
    }
}