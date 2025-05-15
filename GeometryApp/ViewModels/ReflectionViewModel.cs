using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeometryLibrary;

namespace GeometryApp.ViewModels;

public partial class ReflectionViewModel : ObservableObject
{
    [ObservableProperty]
    private string _assemblyPath = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> _availableTypes = new();

    [ObservableProperty]
    private string _selectedType = string.Empty;

    [ObservableProperty]
    private ObservableCollection<MethodInfo> _availableMethods = new();

    [ObservableProperty]
    private MethodInfo? _selectedMethod;

    [ObservableProperty]
    private ObservableCollection<ParameterViewModel> _methodParameters = new();

    [ObservableProperty]
    private string _lastResult = string.Empty;

    private Assembly? _loadedAssembly;
    private Type? _selectedTypeInfo;

    [RelayCommand]
    public void LoadAssembly()
    {
        if (string.IsNullOrEmpty(AssemblyPath) || !File.Exists(AssemblyPath))
        {
            Console.WriteLine($"[DEBUG] AssemblyPath is empty or file does not exist: {AssemblyPath}");
            return;
        }

        try
        {
            _loadedAssembly = Assembly.LoadFrom(AssemblyPath);
            var types = _loadedAssembly.GetTypes()
                .Where(t => typeof(IGeometricFigure).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => t.FullName ?? t.Name)
                .ToList();

            Console.WriteLine($"[DEBUG] Loaded assembly: {AssemblyPath}");
            Console.WriteLine($"[DEBUG] Found types: {string.Join(", ", types)}");

            AvailableTypes.Clear();
            foreach (var type in types)
            {
                AvailableTypes.Add(type);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Error loading assembly: {ex.Message}");
        }
    }

    private bool CanLoadAssembly()
    {
        return true; // Always allow clicking the button to show file dialog
    }

    partial void OnAssemblyPathChanged(string value)
    {
        LoadAssemblyCommand.NotifyCanExecuteChanged();
    }

    partial void OnSelectedTypeChanged(string value)
    {
        Console.WriteLine($"[DEBUG] OnSelectedTypeChanged: {value}");
        if (_loadedAssembly == null || string.IsNullOrEmpty(value))
            return;

        _selectedTypeInfo = _loadedAssembly.GetType(value);
        Console.WriteLine($"[DEBUG] _selectedTypeInfo: {_selectedTypeInfo}");
        if (_selectedTypeInfo == null)
            return;

        var methods = _selectedTypeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => !m.IsSpecialName); // Exclude properties

        AvailableMethods.Clear();
        foreach (var method in methods)
        {
            AvailableMethods.Add(method);
        }
    }

    partial void OnSelectedMethodChanged(MethodInfo? value)
    {
        Console.WriteLine($"[DEBUG] OnSelectedMethodChanged: {value}");
        MethodParameters.Clear();
        if (value == null)
            return;

        foreach (var param in value.GetParameters())
        {
            MethodParameters.Add(new ParameterViewModel
            {
                Name = param.Name ?? "Unknown",
                Type = param.ParameterType,
                Value = GetDefaultValue(param.ParameterType)
            });
        }
    }

    public void ExecuteMethod()
    {
        if (_selectedTypeInfo == null || SelectedMethod == null)
        {
            LastResult = "[ERROR] No type or method selected";
            return;
        }

        try
        {
            var instance = Activator.CreateInstance(_selectedTypeInfo);
            if (instance == null)
            {
                LastResult = "[ERROR] Could not create instance.";
                return;
            }

            var parameters = MethodParameters
                .Select(p => Convert.ChangeType(p.Value, p.Type))
                .ToArray();

            var result = SelectedMethod.Invoke(instance, parameters);
            LastResult = result?.ToString() ?? "null";
        }
        catch (Exception ex)
        {
            LastResult = $"[ERROR] {ex.Message}";
            Console.WriteLine($"Error executing method: {ex.Message}");
        }
    }

    private bool CanExecuteMethod()
    {
        return _selectedTypeInfo != null && SelectedMethod != null;
    }

    private static object? GetDefaultValue(Type type)
    {
        if (type == typeof(string))
            return string.Empty;
        if (type == typeof(int))
            return 0;
        if (type == typeof(double))
            return 0.0;
        if (type == typeof(bool))
            return false;
        return null;
    }
}

public class ParameterViewModel : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public Type Type { get; set; } = typeof(object);
    public object? Value { get; set; }
} 