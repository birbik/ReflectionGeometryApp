using Avalonia;
using System;

namespace GeometryLibrary;

public class Ellipse : GeometricFigure
{
    public double Width { get; }
    public double Height { get; }

    public Ellipse(double x, double y, double width, double height) : base(x, y)
    {
        Width = width;
        Height = height;
    }

    public Ellipse() : base(10, 10) { Width = 20; Height = 30; }

    public override Rect GetBoundingBox() => new Rect(X - Width / 2, Y - Height / 2, Width, Height);
    public override double CalculateArea() => Math.PI * Width * Height / 4;
} 