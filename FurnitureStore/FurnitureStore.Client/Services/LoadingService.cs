using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class LoadingService
{
    private readonly IJSRuntime _jsRuntime;
    private bool _isLoading;
    
    public LoadingService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ShowLoadingAsync()
    {
        _isLoading = true;
        await _jsRuntime.InvokeVoidAsync("showLoading");
    }

    public async Task HideLoadingAsync()
    {
        _isLoading = false;
        await _jsRuntime.InvokeVoidAsync("hideLoading");
    }

    public async Task ExecuteWithLoadingAsync(Func<Task> action)
    {
        try
        {
            await ShowLoadingAsync();
            await action();
        }
        finally
        {
            await HideLoadingAsync();
        }
    }

    public async Task<T> ExecuteWithLoadingAsync<T>(Func<Task<T>> action)
    {
        try
        {
            await ShowLoadingAsync();
            return await action();
        }
        finally
        {
            await HideLoadingAsync();
        }
    }
} 