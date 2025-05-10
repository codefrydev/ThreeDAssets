using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeDAssets.Models;
using ThreeDAssets.Models.Generation;

namespace ThreeDAssets.Components;

public partial class DataGenComponent : ComponentBase
{
    private DataModel dataModelRefeModel = new();
    [Inject] private IJSRuntime JS { get; set; } = null!;

    private string? deserializedJson; // For showing in modal

    private async Task HandleValidSubmit()
    {
        var dataModel = new DataRequestModel
        {
            AltText = dataModelRefeModel.AltText.Value,
            Description = dataModelRefeModel.Description.Value,
            IosSrc = dataModelRefeModel.IosSrc.Value,
            IsFavourite = dataModelRefeModel.IsFavourite,
            Name = dataModelRefeModel.Name.Value,
            Poster = dataModelRefeModel.Poster.Value,
            Source = dataModelRefeModel.Source.Value,
            Thumbnail = dataModelRefeModel.Thumbnail.Value,
            AnimationNames = dataModelRefeModel.AnimationNames.Select(x => x.Value).ToList(),
            EnvironmentImages = dataModelRefeModel.EnvironmentImages.Select(x => x.Value).ToList(),
            SkyboxImages = dataModelRefeModel.SkyboxImages.Select(x => x.Value).ToList()
        };

        var json = JsonSerializer.Serialize(dataModel, new JsonSerializerOptions { WriteIndented = true });
        await JS.InvokeVoidAsync("navigator.clipboard.writeText", json);

        deserializedJson = json;
        await JS.InvokeVoidAsync("bootstrapInterop.showModal", "#jsonModal");
    }


    private void AddAnimation()
    {
        dataModelRefeModel.AnimationNames.Add(new ModelReference());
    }

    private void RemoveAnimation(ModelReference modelReference)
    {
        dataModelRefeModel.AnimationNames.Remove(modelReference);
    }

    private void AddEnvironment()
    {
        dataModelRefeModel.EnvironmentImages.Add(new ModelReference());
    }

    private void RemoveEnvironment(ModelReference modelReference)
    {
        dataModelRefeModel.EnvironmentImages.Remove(modelReference);
    }

    private void AddSkybox()
    {
        dataModelRefeModel.SkyboxImages.Add(new ModelReference());
    }

    private void RemoveSkybox(ModelReference modelReference)
    {
        dataModelRefeModel.SkyboxImages.Remove(modelReference);
    }
}