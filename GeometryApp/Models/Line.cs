using Avalonia;
using System;

namespace GeometryApp.Models;

public class Line : GeometricFigure
{
    public double X2 { get; }
    public double Y2 { get; }

    public Line(double x1, double y1, double x2, double y2) 
        : base((x1 + x2) / 2, (y1 + y2) / 2) 
    {
        X2 = x2;
        Y2 = y2;
    }

    public override Rect GetBoundingBox()
    {
        double x1 = X * 2 - X2; 
        double y1 = Y * 2 - Y2;
        
        return new Rect(
            Math.Min(x1, X2),
            Math.Min(y1, Y2),
            Math.Abs(X2 - x1),
            Math.Abs(Y2 - y1)
        );
    }

    public override double CalculateArea() => 0; 
}