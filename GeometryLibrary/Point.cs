using Avalonia;
using System;

namespace GeometryLibrary;

public class Point : GeometricFigure
{
    public Point(double x, double y) : base(x, y)
    {
    }

    public Point() : base(10, 10) { }

    public override Rect GetBoundingBox() => new Rect(X, Y, 0, 0);

    public override double CalculateArea() => 0;
} 