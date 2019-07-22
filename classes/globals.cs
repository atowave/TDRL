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
    public static class globals
    {
        public static Canvas canvas;
        public static Player player = new Player();
        public static baselevel currentLevel;
        public static double default_step = 10;
        public static double step;
        public static Point mouse_position;
        public static Point middlepoint { get {
                return new Point(
                    globals.canvas.ActualWidth/2,
                    globals.canvas.ActualHeight/2
                );
            } }
        public static Label Debug = new Label { Content = "Current Top Pos: ", Foreground = Brushes.White, FontSize = 50 };
    }
    public class Player
    {
        public Canvas visual = new Canvas { Width = 70, Height = 70, Background = Brushes.Red };
        public double height { get { return visual.ActualHeight; } }
        public void UpdatePosition()
        {
            Canvas.SetTop(globals.currentLevel.canvas, globals.currentLevel.public_location.Y);
            Canvas.SetLeft(globals.currentLevel.canvas, globals.currentLevel.public_location.X);
        }
    }
    public class Location
    {
        public double X;
        public double Y;
        public Location(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public void update(double x, double y)
        {
            this.X = x;
            Y = y;
        }
        public Location copy()
        {
            return new Location(X, Y);
        }
    }
}
