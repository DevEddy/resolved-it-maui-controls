using CommunityToolkit.Mvvm.ComponentModel;
using Resolved.It.Maui.App.Services;

namespace Resolved.It.Maui.App.ViewModels;

public class BasePageViewModel : ObservableObject, IQueryAttributable
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
    
    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        
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