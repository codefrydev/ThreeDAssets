using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeDAssets.Models;
using ThreeDAssets.Models.Generation;

namespace ThreeDAssets.Components;

public partial class DataGenComponent : ComponentBase
{
    private readonly DataModel _dataModelRefeModel = new();
    [Inject] private IJSRuntime Js { get; set; } = null!;

    private string? _deserializedJson; // For showing in modal

    private async Task HandleValidSubmit()
    {
        var dataModel = new DataRequestModel
        {
            AltText = _dataModelRefeModel.AltText.Value,
            Description = _dataModelRefeModel.Description.Value,
            IosSrc = _dataModelRefeModel.IosSrc.Value,
            IsFavourite = _dataModelRefeModel.IsFavourite,
            Name = _dataModelRefeModel.Name.Value,
            Poster = _dataModelRefeModel.Poster.Value,
            Source = _dataModelRefeModel.Source.Value,
            Thumbnail = _dataModelRefeModel.Thumbnail.Value,
            AnimationNames = _dataModelRefeModel.AnimationNames.Select(x => x.Value).ToList(),
            EnvironmentImages = _dataModelRefeModel.EnvironmentImages.Select(x => x.Value).ToList(),
            SkyboxImages = _dataModelRefeModel.SkyboxImages.Select(x => x.Value).ToList()
        };

        var json = JsonSerializer.Serialize(dataModel, options: new JsonSerializerOptions { WriteIndented = true });
        await Js.InvokeVoidAsync("navigator.clipboard.writeText", json);

        _deserializedJson = json;
        await Js.InvokeVoidAsync("bootstrapInterop.showModal", "#jsonModal");
    }


    private void AddAnimation()
    {
        _dataModelRefeModel.AnimationNames.Add(new ModelReference());
    }

    private void RemoveAnimation(ModelReference modelReference)
    {
        _dataModelRefeModel.AnimationNames.Remove(modelReference);
    }

    private void AddEnvironment()
    {
        _dataModelRefeModel.EnvironmentImages.Add(new ModelReference());
    }

    private void RemoveEnvironment(ModelReference modelReference)
    {
        _dataModelRefeModel.EnvironmentImages.Remove(modelReference);
    }

    private void AddSkybox()
    {
        _dataModelRefeModel.SkyboxImages.Add(new ModelReference());
    }

    private void RemoveSkybox(ModelReference modelReference)
    {
        _dataModelRefeModel.SkyboxImages.Remove(modelReference);
    }
}