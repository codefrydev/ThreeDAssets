using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class GoToFullScreen : ComponentBase
{
    [Inject] private IJSRuntime Js { get; set; } = null!;
    [Parameter] public string ElementId { get; set; } = "model-viewer";
    [Parameter] public ModelViewer ModelData { get; set; } = null!;
    private bool _isFullScreen;
    private DotNetObjectReference<GoToFullScreen>? _objRef;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _objRef = DotNetObjectReference.Create(this);
            await Js.InvokeVoidAsync("registerFullScreenCallback", _objRef, ElementId);
        }
    }

    private async Task GoFull()
    {
        _isFullScreen = true;
        StateHasChanged();
        await Js.InvokeVoidAsync("openFullScreen", ElementId);
    }

    private async Task GoExit()
    {
        _isFullScreen = false;
        StateHasChanged();
        await Js.InvokeVoidAsync("closeFullScreen");
    }

    public void Dispose()
    {
        _objRef?.Dispose();
    }

    [JSInvokable]
    public void SetFullScreenState(bool isFull)
    {
        _isFullScreen = isFull;
        StateHasChanged();
    }

    protected override void OnParametersSet()
    {
        // Reset full screen state if model changes
        _isFullScreen = false;
    }
}