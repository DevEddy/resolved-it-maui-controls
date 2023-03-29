using CommunityToolkit.Mvvm.ComponentModel;
using Resolved.It.Maui.App.Services;

namespace Resolved.It.Maui.App.ViewModels;

public class BasePageViewModel : ObservableObject
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        private set => SetProperty(ref _isBusy, value);
    }

    public INavigationService NavigationService { get; }

    public BasePageViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
    
    protected async Task IsBusyFor(Func<Task> unitOfWork)
    {
        await _isBusyLock.WaitAsync();

        try
        {
            IsBusy = true;

            await unitOfWork();
        }
        finally
        {
            IsBusy = false;
            _isBusyLock.Release();
        }
    }
}