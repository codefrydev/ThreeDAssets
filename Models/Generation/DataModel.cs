namespace ThreeDAssets.Models.Generation;

public class DataModel
{
    public bool IsFavourite { get; set; } = false;
    public ModelReference Name { get; set; } = new();
    public ModelReference Thumbnail { get; set; } = new();
    public ModelReference Description { get; set; } = new();

    public ModelReference Source { get; set; } = new();

    public ModelReference Poster { get; set; } = new();
    public ModelReference AltText { get; set; } = new();
    public ModelReference IosSrc { get; set; } = new();

    public List<ModelReference> AnimationNames { get; set; } = [];

    public List<ModelReference> EnvironmentImages { get; set; } = [];

    public List<ModelReference> SkyboxImages { get; set; } = [];
}