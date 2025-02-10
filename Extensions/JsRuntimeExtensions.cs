using Microsoft.JSInterop;

namespace YumBlazor.Extensions;

public static class JsRuntimeExtensions
{
    public static async Task ToasrtSuccess(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public static async Task ToasrtError(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "error", message);
    }
}