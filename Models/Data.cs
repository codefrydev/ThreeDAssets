namespace ThreeDAssets.Models;

public class Data
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsFavourite { get; set; } = false;
    public ModelViewer? ModelViewer { get; set; }
    public ModelViewerOptionsList? ModelViewerOptions { get; set; }
}