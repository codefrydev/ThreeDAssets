using System.ComponentModel.DataAnnotations;

namespace ThreeDAssets.Models.Generation;

public class ModelReference
{
    [Required] public string Value { get; set; }
}