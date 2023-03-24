using CommunityToolkit.Mvvm.ComponentModel;

namespace Resolved.It.Maui.App;

public class BasePageViewModel : ObservableObject
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        private set => SetProperty(ref _isBusy, value);
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