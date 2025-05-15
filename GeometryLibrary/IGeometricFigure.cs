using Avalonia;

namespace GeometryLibrary;

public interface IGeometricFigure
{
    double X { get; }
    double Y { get; }
    string DisplayName { get; }
    double Area { get; }
    string Center { get; }
    string BoundingBoxInfo { get; }
    Rect GetBoundingBox();
    double CalculateArea();
} 