using Resolved.It.Maui.App.ViewModels;

namespace Resolved.It.Maui.App.Views;

public partial class MainPage
{
    public MainPage(MainPageViewModel nextPageViewModel)
    {
        BindingContext = nextPageViewModel;
        InitializeComponent();
    }
}
