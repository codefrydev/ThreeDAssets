using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;

namespace ThreeDAssets.Components;

public partial class Thumbnail : ComponentBase
{
    [Parameter] public Data DataToBeSent { get; set; }
    [Parameter] public EventCallback<string> OnClick { get; set; }
    [Parameter] public string BsModalName { get; set; } = "#exampleModal";
    
    private async Task HandleClick()
    {
        await OnClick.InvokeAsync(DataToBeSent.ProjectName);
    }
}