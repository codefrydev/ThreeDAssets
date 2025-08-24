using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class ThreeDModelViewer : ComponentBase
{
    [Parameter] [EditorRequired] public ModelViewer Model { get; set; } = new();
    [Parameter] public ModelViewerOptionsList Options { get; set; } = new();
}