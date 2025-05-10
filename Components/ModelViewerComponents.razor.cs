using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class ModelViewerComponents : ComponentBase
{
    [Parameter] [EditorRequired] public ModelViewer Model { get; set; } = null!;
    [Parameter] [EditorRequired] public ModelViewerOptionsList Options { get; set; } = new();
    [Parameter] [EditorRequired] public string BsModalName { get; set; } = null!;
}