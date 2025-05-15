using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryLibrary;

public class Polygon : GeometricFigure
{
    private readonly List<Point> _points;

    public Polygon(IEnumerable<Point> points) : base(0, 0)
    {
        _points = points.ToList();
    }

    public Polygon() : base(10, 10)
    {
        _points = new List<Point>
        {
            new Point(10, 10),
            new Point(20, 10),
            new Point(15, 20)
        };
    }

    public override Rect GetBoundingBox()
    {
        if (_points.Count == 0)
            return new Rect();
        double minX = _points.Min(p => p.X);
        double minY = _points.Min(p => p.Y);
        double maxX = _points.Max(p => p.X);
        double maxY = _points.Max(p => p.Y);
        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    public override double CalculateArea()
    {
        if (_points.Count < 3)
            return 0;
        double area = 0;
        for (int i = 0; i < _points.Count; i++)
        {
            var p1 = _points[i];
            var p2 = _points[(i + 1) % _points.Count];
            area += (p1.X * p2.Y) - (p2.X * p1.Y);
        }
        return Math.Abs(area) / 2.0;
    }
} 