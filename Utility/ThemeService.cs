using Microsoft.JSInterop;

namespace ThreeDAssets.Utility;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    public event Action? OnThemeChanged;

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeThemeAsync()
    {
        try
        {
            var theme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
            if (string.IsNullOrEmpty(theme))
            {
                // Check system preference
                var prefersDark =
                    await _jsRuntime.InvokeAsync<bool>("window.matchMedia", "(prefers-color-scheme: dark)");
                theme = prefersDark ? "dark" : "light";
            }

            await SetThemeAsync(theme);
        }
        catch
        {
            // Fallback to light theme if JS is not available
            await SetThemeAsync("light");
        }
    }

    public async Task SetThemeAsync(string theme)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", theme);
            await _jsRuntime.InvokeVoidAsync("document.documentElement.classList.toggle", "dark", theme == "dark");
            OnThemeChanged?.Invoke();
        }
        catch
        {
            // Handle error silently
        }
    }

    public async Task<string> GetCurrentThemeAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme") ?? "light";
        }
        catch
        {
            return "light";
        }
    }

    public async Task ToggleThemeAsync()
    {
        var currentTheme = await GetCurrentThemeAsync();
        var newTheme = currentTheme == "light" ? "dark" : "light";
        await SetThemeAsync(newTheme);
    }
}