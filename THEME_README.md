# 🎨 Theme System Documentation

## Overview

The ThreeDAssets application now includes a comprehensive theme system that supports both light and dark themes. The
system automatically detects user preferences and provides smooth transitions between themes.

## ✨ Features

- **Automatic Theme Detection**: Detects system preference (light/dark)
- **Persistent Storage**: Remembers user's theme choice
- **Smooth Transitions**: CSS transitions for seamless theme switching
- **Theme-Aware Components**: All components automatically adapt to the current theme
- **CSS Custom Properties**: Uses CSS variables for consistent theming

## 🚀 Quick Start

### 1. Theme Toggle Button

The theme toggle button is located in the navigation bar. Click it to switch between light and dark themes.

### 2. Automatic Initialization

The theme system automatically initializes when the app starts, detecting the user's preferred theme.

## 🎯 How to Use

### Theme-Aware CSS Classes

Use these CSS classes in your Razor components for automatic theme adaptation:

```html
<!-- Backgrounds -->
<div class="bg-theme-primary">Primary background</div>
<div class="bg-theme-secondary">Secondary background</div>
<div class="bg-theme-tertiary">Tertiary background</div>

<!-- Text Colors -->
<h1 class="text-theme-primary">Primary text</h1>
<p class="text-theme-secondary">Secondary text</p>
<span class="text-theme-muted">Muted text</span>

<!-- Borders -->
<div class="border border-theme-primary">Primary border</div>
<div class="border border-theme-secondary">Secondary border</div>

<!-- Shadows -->
<div class="shadow-theme-sm">Small shadow</div>
<div class="shadow-theme-lg">Large shadow</div>

<!-- Gradients -->
<div class="bg-gradient-theme">Theme-aware gradient</div>
```

### Theme Service

Inject the `ThemeService` in your components to programmatically control themes:

```csharp
@inject ThemeService ThemeService

<button @onclick="ToggleTheme">Toggle Theme</button>

@code {
    private async Task ToggleTheme()
    {
        await ThemeService.ToggleThemeAsync();
    }
}
```

## 🎨 Theme Colors

### Light Theme

- **Primary Background**: `#ffffff` (White)
- **Secondary Background**: `#f8fafc` (Light Gray)
- **Primary Text**: `#0f172a` (Dark Gray)
- **Secondary Text**: `#475569` (Medium Gray)
- **Borders**: `#e2e8f0` (Light Gray)
- **Accent**: `#3b82f6` (Blue)

### Dark Theme

- **Primary Background**: `#0f172a` (Dark Blue)
- **Secondary Background**: `#1e293b` (Medium Blue)
- **Primary Text**: `#f8fafc` (Light Gray)
- **Secondary Text**: `#cbd5e1` (Medium Gray)
- **Borders**: `#334155` (Dark Gray)
- **Accent**: `#60a5fa` (Light Blue)

## 🔧 Implementation Details

### CSS Variables

The theme system uses CSS custom properties defined in `wwwroot/css/themes.css`:

```css
:root {
    --bg-primary: #ffffff;
    --text-primary: #0f172a;
    /* ... more variables */
}

.dark {
    --bg-primary: #0f172a;
    --text-primary: #f8fafc;
    /* ... more variables */
}
```

### JavaScript Integration

Theme initialization script prevents flash of unstyled content:

```javascript
(function() {
    const theme = localStorage.getItem('theme') || 
                  (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
    document.documentElement.classList.toggle('dark', theme === 'dark');
})();
```

### Blazor Service

The `ThemeService` manages theme state and persistence:

```csharp
public class ThemeService
{
    public async Task InitializeThemeAsync();
    public async Task SetThemeAsync(string theme);
    public async Task<string> GetCurrentThemeAsync();
    public async Task ToggleThemeAsync();
    public event Action? OnThemeChanged;
}
```

## 📱 Responsive Design

The theme system works seamlessly across all device sizes and includes:

- **Mobile Navigation**: Theme toggle in mobile menu
- **Touch-Friendly**: Optimized for touch devices
- **Accessibility**: High contrast ratios in both themes

## 🎭 Customization

### Adding New Theme Colors

1. Add new CSS variables to `themes.css`:

```css
:root {
    --custom-color: #your-color;
}

.dark {
    --custom-color: #your-dark-color;
}
```

2. Create utility classes:

```css
.bg-custom { background-color: var(--custom-color); }
.text-custom { color: var(--custom-color); }
```

### Component Integration

To make a component theme-aware:

1. Replace hardcoded colors with theme classes
2. Use CSS variables for custom styling
3. Test in both light and dark themes

## 🧪 Testing

### Theme Demo Page

Visit `/theme-demo` to see all theme features in action:

- Color palette demonstration
- Interactive elements
- Form components
- Navigation examples

### Development Testing

1. **Light Theme**: Default browser appearance
2. **Dark Theme**: Click theme toggle button
3. **System Preference**: Change OS theme setting
4. **Persistence**: Refresh page to verify theme memory

## 🐛 Troubleshooting

### Common Issues

1. **Theme not switching**: Check browser console for JavaScript errors
2. **Flash of unstyled content**: Ensure theme CSS is loaded before app initialization
3. **Inconsistent colors**: Verify all hardcoded colors are replaced with theme classes

### Debug Mode

Enable debug logging in the ThemeService:

```csharp
// Add logging to ThemeService methods
_logger.LogInformation("Theme changed to: {Theme}", theme);
```

## 📚 Examples

### Card Component

```html
<div class="bg-theme-primary border border-theme-primary rounded-xl shadow-theme-md p-6">
    <h3 class="text-theme-primary font-bold mb-2">Card Title</h3>
    <p class="text-theme-secondary">Card content with theme-aware styling.</p>
    <button class="mt-4 px-4 py-2 bg-theme-accent text-white rounded-lg hover:bg-theme-accent/80">
        Action Button
    </button>
</div>
```

### Navigation Menu

```html
<nav class="bg-theme-primary border-b border-theme-primary">
    <a href="#" class="text-theme-secondary hover:text-theme-accent hover:bg-theme-secondary px-4 py-2 rounded-lg transition-all duration-200">
        Navigation Link
    </a>
</nav>
```

## 🤝 Contributing

When adding new components or updating existing ones:

1. **Use theme classes**: Replace hardcoded colors with `bg-theme-*`, `text-theme-*`, etc.
2. **Test both themes**: Ensure components look good in light and dark modes
3. **Maintain consistency**: Follow the established color palette
4. **Update documentation**: Add examples for new theme features

## 📄 License

This theme system is part of the ThreeDAssets project and follows the same licensing terms.

---

For more information, visit the [Theme Demo](/theme-demo) page or check the main [Documentation](/docs).
