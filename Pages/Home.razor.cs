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
    private List<Data> _currentPageModels = [];
    private bool _isLoading = true;
    private bool _isPopupVisible = false;
    
    // Pagination properties
    private int _currentPage = 1;
    private int _pageSize = 12; // Show 12 models per page
    private int _totalPages = 1;
    private readonly int[] _pageSizeOptions = { 6, 12, 24, 48 };

    private void LoadContent(Data conData)
    {
        _modelData = conData.ModelViewer;
        _option = conData.ModelViewerOptions;
        _isPopupVisible = true;
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        
        // Subscribe to favorite state changes
        Helper.FavoriteStateChanged += OnFavoriteStateChanged;
        
        await FetchDataAsync();
        UpdateDisplayedModels();
        if (_allModelDataFromGithub.Count > 0)
        {
            _modelData = _allModelDataFromGithub.FirstOrDefault()?.ModelViewer;
        }
        _isLoading = false;
        await base.OnInitializedAsync();
    }

    private void OnFavoriteStateChanged()
    {
        UpdateDisplayedModels();
        InvokeAsync(StateHasChanged);
    }

    private void UpdateDisplayedModels()
    {
        _allModelDataFromGithub = Helper.IsFavouriteClicked ? Helper.FavouriteModelData : Helper.AllModelData;
        _currentPage = 1; // Reset to first page when filtering
        UpdatePagination();
    }
    
    private void UpdatePagination()
    {
        _totalPages = (int)Math.Ceiling((double)_allModelDataFromGithub.Count / _pageSize);
        _totalPages = Math.Max(1, _totalPages); // Ensure at least 1 page
        _currentPage = Math.Min(_currentPage, _totalPages); // Ensure current page is valid
        
        var startIndex = (_currentPage - 1) * _pageSize;
        var endIndex = Math.Min(startIndex + _pageSize, _allModelDataFromGithub.Count);
        
        if (startIndex < _allModelDataFromGithub.Count)
        {
            _currentPageModels = _allModelDataFromGithub.GetRange(startIndex, endIndex - startIndex);
        }
        else
        {
            _currentPageModels = new List<Data>();
        }
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
        if (_currentPage < _totalPages)
        {
            GoToPage(_currentPage + 1);
        }
    }
    
    private void GoToPreviousPage()
    {
        if (_currentPage > 1)
        {
            GoToPage(_currentPage - 1);
        }
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
        // Unsubscribe from events to prevent memory leaks
        Helper.FavoriteStateChanged -= OnFavoriteStateChanged;
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