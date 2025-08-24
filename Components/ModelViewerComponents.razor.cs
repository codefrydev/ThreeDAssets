using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class ModelViewerComponents : ComponentBase
{
    [Parameter] public ModelViewer? Model { get; set; }
    [Parameter] public ModelViewerOptionsList? Options { get; set; }
    [Parameter] public string BsModalName { get; set; } = string.Empty;
    [Parameter] public bool IsVisible { get; set; } = false;
    [Parameter] public EventCallback OnClose { get; set; }

    private void ClosePopup()
    {
        OnClose.InvokeAsync();
    }
}