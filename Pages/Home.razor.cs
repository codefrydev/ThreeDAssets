using System.Text.Json;
using ThreeDAssets.Models;
using ThreeDAssets.Utility;

namespace ThreeDAssets.Pages;

public partial class Home
{
    private const string BsModalName = "projectModal";

    private const string AssetsDatabaseJson =
        "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model/index.json";

    private ModelViewer? _modelData;
    private ModelViewerOptionsList? _option;
    private List<Data> _allModelDataFromGithub = [];
    private bool _isLoading = true;

    private void LoadContent(Data conData)
    {
        _modelData = conData.ModelViewer;
        _option = conData.ModelViewerOptions;
        StateHasChanged();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        _allModelDataFromGithub = Helper.IsFavouriteClicked ? Helper.FavouriteModelData : Helper.AllModelData;
        return base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        await FetchDataAsync();
        _isLoading = false;
        await base.OnInitializedAsync();
    }

    private async Task FetchDataAsync()
    {
        using var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(AssetsDatabaseJson);

            if (response.IsSuccessStatusCode)
            {
                var videoListString = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(videoListString))
                {
                    var dataRequestModels = JsonSerializer.Deserialize<List<DataRequestModel>>(videoListString);

                    if (dataRequestModels != null)
                    {
                        foreach (var dataRequestModel in dataRequestModels)
                        {
                            var data = new Data
                            {
                                Name = dataRequestModel.Name,
                                ImageUrl = dataRequestModel.Thumbnail,
                                Description = dataRequestModel.Description,
                                IsFavourite = dataRequestModel.IsFavourite,
                                ModelViewer = new ModelViewer
                                {
                                    Source = dataRequestModel.Source,
                                    Poster = dataRequestModel.Poster,
                                    AltText = dataRequestModel.AltText,
                                    IosSrc = dataRequestModel.IosSrc
                                },
                                ModelViewerOptions = new ModelViewerOptionsList
                                {
                                    AnimationNames = dataRequestModel.AnimationNames,
                                    EnvironmentImages = dataRequestModel.EnvironmentImages,
                                    SkyboxImages = dataRequestModel.SkyboxImages,
                                    SkyboxHeights = dataRequestModel.SkyboxImages
                                }
                            };
                            if (dataRequestModel.IsFavourite) Helper.FavouriteModelData.Add(data);
                            Helper.AllModelData.Add(data);
                        }
                    }
                    else
                    {
                        _allModelDataFromGithub = [];
                        Console.WriteLine("Deserialization returned null.");
                    }
                }
                else
                {
                    Console.WriteLine("Empty or invalid JSON response.");
                }
            }
            else
            {
                Console.WriteLine($"HTTP request failed with status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON deserialization error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}