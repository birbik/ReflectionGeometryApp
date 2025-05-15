using Avalonia;
using System.Globalization;

namespace GeometryLibrary;

public abstract class GeometricFigure : IGeometricFigure
{
    public double X { get; protected set; }
    public double Y { get; protected set; }
    public virtual string DisplayName => GetType().Name;
    public double Area => CalculateArea(); 
    
    public string Center => 
        $"{X.ToString("0.##", CultureInfo.InvariantCulture)}, " +
        $"{Y.ToString("0.##", CultureInfo.InvariantCulture)}";

    public string BoundingBoxInfo => 
        $"X={GetBoundingBox().X:0.##}, Y={GetBoundingBox().Y:0.##}, " +
        $"W={GetBoundingBox().Width:0.##}, H={GetBoundingBox().Height:0.##}";

    protected GeometricFigure(double x, double y)
    {
        X = x;
        Y = y;
    }

    public abstract Rect GetBoundingBox();
    public abstract double CalculateArea();
} 