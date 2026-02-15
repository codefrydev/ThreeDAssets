using System.Text.Json;
using Microsoft.AspNetCore.Components;
using ThreeDAssets.Models;
using ThreeDAssets.Utility;

namespace ThreeDAssets.Pages;

public partial class Home : IDisposable
{
    private const string BsModalName = "projectModal";

    private const string AssetsDatabaseJson =
        "https://raw.githubusercontent.com/abhijeetunreal/3DB/refs/heads/main/model/index.json";

    private ModelViewer? _modelData;
    private ModelViewerOptionsList? _option;
    private List<Data> _allModelDataFromGithub = [];
    private List<Data> _filteredList = [];
    private List<Data> _currentPageModels = [];
    private bool _isLoading = true;
    private bool _isPopupVisible = false;

    // Pagination
    private int _currentPage = 1;
    private int _pageSize = 12;
    private int _totalPages = 1;
    private readonly int[] _pageSizeOptions = { 6, 12, 24, 48 };

    // Sort: "name-asc" | "name-desc"
    private string _sortOption = "name-asc";

    // Grid size: "small" | "medium" | "large"
    private string _gridSize = "medium";

    private void LoadContent(Data conData)
    {
        // Ensure a new instance is assigned so Blazor re-renders with the correct model
        _modelData = new ModelViewer
        {
            Source = conData.ModelViewer.Source,
            Poster = conData.ModelViewer.Poster,
            AltText = conData.ModelViewer.AltText,
            IosSrc = conData.ModelViewer.IosSrc,
            EnvironmentImage = conData.ModelViewer.EnvironmentImage,
            SkyboxImage = conData.ModelViewer.SkyboxImage,
            BackgroundColor = conData.ModelViewer.BackgroundColor,
            CameraControls = conData.ModelViewer.CameraControls,
            AutoRotate = conData.ModelViewer.AutoRotate,
            DisablePan = conData.ModelViewer.DisablePan,
            DisableTap = conData.ModelViewer.DisableTap,
            AnimationName = conData.ModelViewer.AnimationName,
            AnimationCrossfadeDuration = conData.ModelViewer.AnimationCrossfadeDuration,
            CameraOrbit = conData.ModelViewer.CameraOrbit,
            MaxCameraOrbit = conData.ModelViewer.MaxCameraOrbit,
            MinCameraOrbit = conData.ModelViewer.MinCameraOrbit,
            FieldOfView = conData.ModelViewer.FieldOfView,
            MaxFieldOfView = conData.ModelViewer.MaxFieldOfView,
            MinFieldOfView = conData.ModelViewer.MinFieldOfView,
            SkyboxHeight = conData.ModelViewer.SkyboxHeight,
            ToneMapping = conData.ModelViewer.ToneMapping,
            ShadowIntensity = conData.ModelViewer.ShadowIntensity,
            ShadowSoftness = conData.ModelViewer.ShadowSoftness,
            Exposure = conData.ModelViewer.Exposure,
            Reveal = conData.ModelViewer.Reveal,
            Loading = conData.ModelViewer.Loading,
            TouchAction = conData.ModelViewer.TouchAction,
            EnableAr = conData.ModelViewer.EnableAr,
            ArModes = conData.ModelViewer.ArModes,
            ArScale = conData.ModelViewer.ArScale
        };
        _option = conData.ModelViewerOptions;
        Helper.CurrentSelectedModel = _modelData;
        _isPopupVisible = true;
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;

        Helper.FavoriteStateChanged += OnFavoriteStateChanged;
        Helper.SearchChanged += OnSearchChanged;

        await FetchDataAsync();
        UpdateDisplayedModels();
        if (_allModelDataFromGithub.Count > 0) _modelData = _allModelDataFromGithub.FirstOrDefault()?.ModelViewer;
        _isLoading = false;
        await base.OnInitializedAsync();
    }

    private void OnFavoriteStateChanged()
    {
        UpdateDisplayedModels();
        InvokeAsync(StateHasChanged);
    }

    private void OnSearchChanged()
    {
        UpdateDisplayedModels();
        InvokeAsync(StateHasChanged);
    }

    private void UpdateDisplayedModels()
    {
        _allModelDataFromGithub = Helper.IsFavouriteClicked ? Helper.FavouriteModelData : Helper.AllModelData;

        var query = (Helper.SearchQuery ?? "").Trim().ToLowerInvariant();
        _filteredList = string.IsNullOrEmpty(query)
            ? _allModelDataFromGithub.ToList()
            : _allModelDataFromGithub.Where(d =>
                (d.Name?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (d.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

        _filteredList = _sortOption == "name-desc"
            ? _filteredList.OrderByDescending(d => d.Name).ToList()
            : _filteredList.OrderBy(d => d.Name).ToList();

        _currentPage = 1;
        UpdatePagination();
    }

    private void UpdatePagination()
    {
        _totalPages = (int)Math.Ceiling((double)_filteredList.Count / _pageSize);
        _totalPages = Math.Max(1, _totalPages);
        _currentPage = Math.Min(_currentPage, _totalPages);

        var startIndex = (_currentPage - 1) * _pageSize;
        var endIndex = Math.Min(startIndex + _pageSize, _filteredList.Count);

        if (startIndex < _filteredList.Count)
            _currentPageModels = _filteredList.GetRange(startIndex, endIndex - startIndex);
        else
            _currentPageModels = new List<Data>();
    }

    private void ApplySort(string option)
    {
        _sortOption = option;
        UpdateDisplayedModels();
        StateHasChanged();
    }

    private void ApplyGridSize(string size)
    {
        _gridSize = size;
        StateHasChanged();
    }

    private void GoToPage(int page)
    {
        if (page >= 1 && page <= _totalPages && page != _currentPage)
        {
            _currentPage = page;
            UpdatePagination();
            StateHasChanged();
        }
    }

    private void GoToNextPage()
    {
        if (_currentPage < _totalPages) GoToPage(_currentPage + 1);
    }

    private void GoToPreviousPage()
    {
        if (_currentPage > 1) GoToPage(_currentPage - 1);
    }

    private void GoToFirstPage()
    {
        GoToPage(1);
    }

    private void GoToLastPage()
    {
        GoToPage(_totalPages);
    }

    private void ChangePageSize(int newPageSize)
    {
        if (newPageSize != _pageSize)
        {
            _pageSize = newPageSize;
            _currentPage = 1; // Reset to first page when changing page size
            UpdatePagination();
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        Helper.FavoriteStateChanged -= OnFavoriteStateChanged;
        Helper.SearchChanged -= OnSearchChanged;
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}