using Avalonia;

namespace GeometryApp.Models;

public class Point : GeometricFigure
{
    public Point(double x, double y) : base(x, y) { }

    public override Rect GetBoundingBox() => new Rect(X, Y, 0, 0);
    public override double CalculateArea() => 0;
}