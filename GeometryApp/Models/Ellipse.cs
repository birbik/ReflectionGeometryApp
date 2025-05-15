using System;
using Avalonia;

namespace GeometryApp.Models;

public class Ellipse : GeometricFigure
{
    public double Width { get; }
    public double Height { get; }

    public Ellipse(double x, double y, double width, double height) 
        : base(x, y)
    {
        Width = width;
        Height = height;
    }

    public override Rect GetBoundingBox()
    {
        return new Rect(X - Width/2, Y - Height/2, Width, Height);
    }

    public override double CalculateArea()
    {
        return Math.PI * Width * Height / 4;
    }
}