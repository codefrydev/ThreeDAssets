using System.ComponentModel.DataAnnotations;

namespace ThreeDAssets.Models.Generation;

public class DataModel
{
    public bool IsFavourite { get; set; } = false;
    [Required] public ModelReference Name { get; set; } = new();

    [Required]
    public ModelReference Thumbnail { get; set; } = new()
    {
        Value = "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model"
    };

    [Required] public ModelReference Description { get; set; } = new();

    [Required]
    public ModelReference Source { get; set; } = new()
    {
        Value = "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model"
    };

    [Required]
    public ModelReference Poster { get; set; } = new()
    {
        Value = "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model"
    };

    [Required] public ModelReference AltText { get; set; } = new();
    [Required] public ModelReference IosSrc { get; set; } = new();

    public List<ModelReference> AnimationNames { get; set; } = [];

    public List<ModelReference> EnvironmentImages { get; set; } = [];

    public List<ModelReference> SkyboxImages { get; set; } = [];
}