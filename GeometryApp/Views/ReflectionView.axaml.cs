using Avalonia.Controls;
using Avalonia.Platform.Storage;
using GeometryApp.ViewModels;
using System.Threading.Tasks;

namespace GeometryApp.Views;

public partial class ReflectionView : UserControl
{
    public ReflectionView()
    {
        InitializeComponent();
        DataContext = new ReflectionViewModel();
    }

    private async void BrowseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var vm = DataContext as ReflectionViewModel;
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null || vm == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Assembly",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("DLL Files") { Patterns = new[] { "*.dll" } },
                new FilePickerFileType("All Files") { Patterns = new[] { "*" } }
            }
        });
        if (files.Count == 0) return;
        vm.AssemblyPath = files[0].Path.LocalPath;
        vm.LoadAssembly();
    }

    private void ExecuteMethod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var vm = DataContext as ReflectionViewModel;
        if (vm == null) return;

        try
        {
            vm.ExecuteMethod();
        }
        catch (System.Exception ex)
        {
            vm.LastResult = $"[ERROR] {ex.Message}";
        }
    }
} 