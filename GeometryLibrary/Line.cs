using Avalonia;
using System;

namespace GeometryLibrary;

public class Line : GeometricFigure
{
    public double EndX { get; }
    public double EndY { get; }

    public Line(double x, double y, double endX, double endY) : base(x, y)
    {
        EndX = endX;
        EndY = endY;
    }

    public Line() : base(10, 10) { EndX = 20; EndY = 20; }

    public override Rect GetBoundingBox()
    {
        double minX = Math.Min(X, EndX);
        double minY = Math.Min(Y, EndY);
        double width = Math.Abs(EndX - X);
        double height = Math.Abs(EndY - Y);
        return new Rect(minX, minY, width, height);
    }

    public override double CalculateArea() => 0;
} 