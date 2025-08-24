using ThreeDAssets.Models;

namespace ThreeDAssets.Utility;

public class Helper
{
    private static bool _isFavouriteClicked = false;
    public static List<Data> AllModelData = [];
    public static List<Data> FavouriteModelData = [];
    
    public static event Action? FavoriteStateChanged;
    
    public static bool IsFavouriteClicked 
    { 
        get => _isFavouriteClicked;
        set 
        {
            if (_isFavouriteClicked != value)
            {
                _isFavouriteClicked = value;
                FavoriteStateChanged?.Invoke();
            }
        }
    }

    public static ModelViewer? CurrentSelectedModel { get; set; }
}