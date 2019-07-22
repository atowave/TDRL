using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MovingEngine.levels;

namespace MovingEngine
{
    public static class Globals
    {
        public static Canvas canvas;
        public static Player player = new Player();
        public static Baselevel currentLevel;
        public const double default_step = 5;
        public static double step;
        public static Point mouse_position;
        public static Point Middlepoint { get {
                return new Point(
                    Globals.canvas.ActualWidth/2,
                    Globals.canvas.ActualHeight/2
                );
            } }
        public static Label Debug = new Label { Content = "Current Pos: ", Foreground = Brushes.White, FontSize = 50 };
    }
    public class Player
    {
        public Location lastLocation = new Location(0, 0);
        public Canvas visual = new Canvas { Width = 70, Height = 70, Background = Brushes.Red };
        public double Height { get { return visual.ActualHeight; } }
        public void UpdatePosition()
        {
            lastLocation = new Location(Canvas.GetLeft(Globals.currentLevel.Canvas), Canvas.GetTop(Globals.currentLevel.Canvas));
            Canvas.SetTop(Globals.currentLevel.Canvas, Globals.currentLevel.Public_location.Y);
            Canvas.SetLeft(Globals.currentLevel.Canvas, Globals.currentLevel.Public_location.X);
        }
        public void UpdatePosition(Location newPos)
        {
            Canvas.SetTop(Globals.currentLevel.Canvas, newPos.Y);
            Canvas.SetLeft(Globals.currentLevel.Canvas, newPos.X);
            Globals.currentLevel.Public_location.X = newPos.X;
            Globals.currentLevel.Public_location.Y = newPos.Y;
        }
        public double rad = 0;
    }
    public static class X
    {
        public static Location ToLocation(this Tuple<double, double> v)
        {
            return new Location(v.Item1, v.Item2);
        }
        public static Location ToLocation(this Point v)
        {
            return new Location(v.X, v.Y);
        }
    }
    public class Location
    {
        public Tuple<double, double> ToTuple()
        {
            return new Tuple<double, double>(X, Y);
        }
        public double X;
        public double Y;
        public Location(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public void Update(double x, double y)
        {
            this.X = x;
            Y = y;
        }
        public override string ToString()
        {
            return "[" + X + " | " + Y + "]";
        }
        public Location Copy()
        {
            return new Location(X, Y);
        }
    }
}
