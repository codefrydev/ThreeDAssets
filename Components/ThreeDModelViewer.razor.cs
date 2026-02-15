using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class ThreeDModelViewer : ComponentBase
{
    [Parameter] [EditorRequired] public ModelViewer Model { get; set; } = new();
    [Parameter] public ModelViewerOptionsList Options { get; set; } = new();
    [Parameter] public bool FillContainer { get; set; }

    /// <summary>
    /// Optional unique id for this viewer. If not set, a unique id is generated so fullscreen targets the correct instance (e.g. popup vs hero).
    /// </summary>
    [Parameter] public string? ViewerId { get; set; }

    private string _viewerId = "";

    protected override void OnInitialized()
    {
        _viewerId = ViewerId ?? $"model-viewer-{Guid.NewGuid():N}";
    }
}