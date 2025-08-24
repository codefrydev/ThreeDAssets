# ThreeDAssets - Modern 3D Model Gallery

A beautiful, responsive web application built with Blazor WebAssembly for showcasing and viewing 3D models with AR
capabilities, built-in data generation tools, and a modern UI.

![ThreeDAssets Preview](https://img.shields.io/badge/ThreeDAssets-3D%20Gallery-blue?style=for-the-badge&logo=blazor)
![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-purple?style=for-the-badge&logo=blazor)
![.NET](https://img.shields.io/badge/.NET-9.0-blue?style=for-the-badge&logo=dotnet)
![Tailwind CSS](https://img.shields.io/badge/Tailwind-CSS-38B2AC?style=for-the-badge&logo=tailwind-css)

## 🌟 Features

- **3D Model Viewer**: Interactive 3D model display with camera controls
- **AR Support**: Augmented Reality viewing capabilities for compatible devices
- **Data Generator**: Built-in tool for creating 3D model metadata
- **Responsive Design**: Modern, mobile-first UI built with Tailwind CSS
- **PWA Support**: Progressive Web App with offline capabilities
- **Model Gallery**: Grid-based layout with pagination and search
- **Favorites System**: Mark and filter favorite 3D models
- **Fullscreen Mode**: Immersive viewing experience

## 🚀 Quick Start

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Modern web browser (Chrome, Firefox, Safari, Edge)
- Git (for cloning)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/codefrydev/ThreeDAssets.git
   cd ThreeDAssets
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Open your browser**
   Navigate to `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure

```
ThreeDAssets/
├── Components/                 # Reusable UI components
│   ├── DataGenComponent.razor # 3D model data generator
│   ├── ThreeDModelViewer.razor # 3D model display component
│   └── ModelViewerComponents.razor # Model viewer wrapper
├── Layout/                    # Page layouts and navigation
│   ├── MainLayout.razor      # Main application layout
│   ├── NavBar.razor          # Navigation bar component
│   └── FooterSection.razor   # Footer component
├── Models/                    # Data models and structures
│   ├── Data.cs               # Core data models
│   ├── ModelViewer.cs        # 3D viewer configuration
│   └── Generation/           # Data generation models
├── Pages/                     # Application pages
│   ├── Home.razor            # Main gallery page
│   └── DataGen.razor         # Data generation page
├── wwwroot/                   # Static assets
│   ├── index.html            # Main HTML file
│   ├── service-worker.js     # PWA service worker
│   └── manifest.webmanifest  # PWA manifest
└── Program.cs                 # Application entry point
```

## 🎯 How to Use

### 1. Browsing 3D Models

- **Home Page**: View all available 3D models in a responsive grid
- **Pagination**: Navigate through models with built-in pagination
- **Page Size**: Adjust how many models to display per page
- **Responsive Grid**: Automatically adapts to different screen sizes

### 2. Viewing 3D Models

- **Click to View**: Click on any model thumbnail to open the 3D viewer
- **Camera Controls**:
    - Left click + drag to rotate
    - Right click + drag to pan
    - Scroll to zoom
- **Fullscreen Mode**: Click the fullscreen button for immersive viewing
- **AR Mode**: Use AR features on compatible mobile devices

### 3. Using the Data Generator

Navigate to `/DataGen` to access the 3D Model Data Generator:

- **Basic Information**: Enter model name, description, and mark as favorite
- **URLs and Sources**: Add thumbnail, GLB source, poster, and iOS source URLs
- **Advanced Settings**: Configure animations, environment images, and skybox images
- **Generate JSON**: Create properly formatted metadata for your 3D models
- **Copy to Clipboard**: Easily copy generated data for use in other applications

### 4. Managing Favorites

- **Mark as Favorite**: Click the star icon on any model
- **Filter Favorites**: Use the favorites filter to view only your marked models
- **Persistent Storage**: Favorites are saved in your browser's local storage

## 🛠️ Customization

### 1. Styling and Theme

The application uses Tailwind CSS for styling. Customize the appearance by:

- **Modifying Tailwind classes** in Razor components
- **Adding custom CSS** in `wwwroot/index.html`
- **Updating color schemes** by changing gradient classes
- **Customizing animations** by modifying Tailwind animation classes

### 2. Adding New Components

1. **Create a new Razor component** in the `Components/` folder
2. **Add the component** to your desired page
3. **Register any services** in `Program.cs` if needed

Example:

```razor
@* Components/MyCustomComponent.razor *@
<div class="bg-white rounded-lg p-4">
    <h3 class="text-lg font-bold">My Custom Component</h3>
    <p>Custom content here</p>
</div>
```

### 3. Modifying the Data Structure

To add new fields to 3D models:

1. **Update the data models** in `Models/Data.cs`
2. **Modify the data generator** in `Components/DataGenComponent.razor`
3. **Update the display components** to show new fields
4. **Regenerate the application** to apply changes

### 4. Adding New Pages

1. **Create a new Razor page** in the `Pages/` folder
2. **Add routing** with `@page "/your-route"`
3. **Update navigation** in `Layout/NavBar.razor`
4. **Add any required models** and services

Example:

```razor
@* Pages/About.razor *@
@page "/about"

<div class="container mx-auto px-4 py-8">
    <h1 class="text-3xl font-bold">About ThreeDAssets</h1>
    <p class="mt-4">Your content here...</p>
</div>
```

### 5. Customizing 3D Viewer Settings

Modify the `ThreeDModelViewer.razor` component to:

- **Change default camera settings**
- **Add new viewer options**
- **Customize AR behavior**
- **Modify lighting and environment settings**

## 🔧 Configuration

### Environment Variables

Create a `appsettings.json` file for configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ModelViewer": {
    "DefaultCameraOrbit": "0deg 75deg 105%",
    "DefaultFieldOfView": "30deg",
    "AutoRotate": true
  }
}
```

### PWA Configuration

Customize the Progressive Web App settings in `wwwroot/manifest.webmanifest`:

```json
{
  "name": "ThreeDAssets",
  "short_name": "3DAssets",
  "description": "Modern 3D Model Gallery",
  "start_url": "/",
  "display": "standalone",
  "background_color": "#ffffff",
  "theme_color": "#3b82f6"
}
```

## 🚀 Deployment

### Local Development

```bash
# Development mode with hot reload
dotnet watch run

# Production build
dotnet build --configuration Release
dotnet run --configuration Release
```

### Docker Deployment

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ThreeDAssets.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build "ThreeDAssets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThreeDAssets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThreeDAssets.dll"]
```

### Azure Static Web Apps

1. **Push to GitHub** with your changes
2. **Connect to Azure Static Web Apps**
3. **Configure build settings**:
    - Build command: `dotnet build --configuration Release`
    - Output location: `bin/Release/net9.0/publish/wwwroot`

## 📱 Browser Support

- **Chrome**: 90+ (Full support)
- **Firefox**: 88+ (Full support)
- **Safari**: 14+ (Full support)
- **Edge**: 90+ (Full support)
- **Mobile browsers**: iOS Safari 14+, Chrome Mobile 90+

## 🧪 Testing

```bash
# Run tests (if configured)
dotnet test

# Build and test
dotnet build
dotnet test
```

## 🤝 Contributing

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Commit your changes**: `git commit -m 'Add amazing feature'`
4. **Push to the branch**: `git push origin feature/amazing-feature`
5. **Open a Pull Request**

### Development Guidelines

- **Follow Blazor best practices**
- **Use Tailwind CSS for styling**
- **Maintain responsive design**
- **Add proper error handling**
- **Include documentation for new features**

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

- **Issues**: [GitHub Issues](https://github.com/codefrydev/ThreeDAssets/issues)
- **Discussions**: [GitHub Discussions](https://github.com/codefrydev/ThreeDAssets/discussions)
- **Documentation**: [Wiki](https://github.com/codefrydev/ThreeDAssets/wiki)

## 🙏 Acknowledgments

- **Blazor Team** for the amazing WebAssembly framework
- **Tailwind CSS** for the utility-first CSS framework
- **Model Viewer** for 3D model rendering capabilities
- **Three.js** for 3D graphics support

## 📊 Performance Tips

- **Optimize 3D models** before uploading (reduce polygon count)
- **Use compressed textures** (WebP, JPEG) for faster loading
- **Implement lazy loading** for large model collections
- **Cache frequently accessed models** in the service worker
- **Use CDN** for static assets when possible

## 🔒 Security Considerations

- **Validate all user inputs** in the data generator
- **Sanitize URLs** before displaying
- **Implement proper CORS** policies for external resources
- **Use HTTPS** in production for secure AR features
- **Regular dependency updates** for security patches

---

**Made with ❤️ using Blazor WebAssembly and Tailwind CSS**

For more information, visit the [live project](https://codefrydev.in/ThreeDAssets/) or check out
the [GitHub repository](https://github.com/codefrydev/ThreeDAssets).
