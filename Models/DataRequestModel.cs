namespace ThreeDAssets.Models;

public class DataRequestModel
{
    public bool IsFavourite { get; set; } = false;
    public string Name { get; set; } = "CFD Models Name";
    public string Thumbnail { get; set; } = "https://codefrydev.in/images/IconCodefrydev.svg";
    public string Description { get; set; } = "A Nerve Plant";

    public string Source { get; set; } =
        "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model/nerveplant.glb";

    public string Poster { get; set; } = "poster.webp";
    public string AltText { get; set; } = "3D Model";
    public string IosSrc { get; set; } = "";

    public List<string> AnimationNames { get; set; } =[];

    public List<string> EnvironmentImages { get; set; } =[];

    public List<string> SkyboxImages { get; set; } =[];
}