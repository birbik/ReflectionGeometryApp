using System.Collections.Generic;
using System.Collections.ObjectModel;
using GeometryApp.Models;

namespace GeometryApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<GeometricFigure> Figures { get; } = new();
        public ReflectionViewModel ReflectionViewModel { get; } = new();

        public MainWindowViewModel()
        {
            Figures.Add(new Point(10, 10));
            Figures.Add(new Line(20, 20, 80, 80));
            Figures.Add(new Ellipse(100, 100, 40, 30));
            Figures.Add(new Polygon(new List<Point>
            {
                new(50, 50),
                new(100, 50),
                new(75, 100)
            }));
        }
    }
}