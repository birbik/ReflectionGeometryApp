using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryApp.Models;

public class Polygon : GeometricFigure
{
    private readonly List<Point> _points;

    public Polygon(IEnumerable<Point> points) 
        : base(points.First().X, points.First().Y)
    {
        _points = points.ToList();
    }

    public override Rect GetBoundingBox()
    {
        var minX = _points.Min(p => p.X);
        var minY = _points.Min(p => p.Y);
        var maxX = _points.Max(p => p.X);
        var maxY = _points.Max(p => p.Y);
        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    public override double CalculateArea()
    {
        double area = 0;
        for (int i = 0; i < _points.Count; i++)
        {
            int j = (i + 1) % _points.Count;
            area += _points[i].X * _points[j].Y;
            area -= _points[j].X * _points[i].Y;
        }
        return Math.Abs(area) / 2;
    }
}