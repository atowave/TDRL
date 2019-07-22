using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MovingEngine.classes;
using MovingEngine.levels;

namespace MovingEngine
{
    public static class Globals
    {
        public static Canvas canvas;
        public static Player player = new Player();
        public static Baselevel currentLevel;
        public static double default_step = 10;
        public static double step;
        public static Point mouse_position;
        public static MouseHandler MouseHandler = new MouseHandler();
        public static List<Projectile> projectiles = new List<Projectile>();
        public static Point Middlepoint { get {
                return new Point(
                    Globals.canvas.ActualWidth/2,
                    Globals.canvas.ActualHeight/2
                );
            } }
        public static Label Debug = new Label { Content = "Current Top Pos: ", Foreground = Brushes.White, FontSize = 50 };
    }
    public class Player
    {
        public Canvas visual = new Canvas { Width = 70, Height = 70, Background = Brushes.Red };
        public double ShootDelay = 10;
        public double ShootDelayCurrent = 0;
        public double Height { get { return visual.ActualHeight; } }
        public void UpdatePosition()
        {
            Canvas.SetTop(Globals.currentLevel.Canvas, Globals.currentLevel.Public_location.Y);
            Canvas.SetLeft(Globals.currentLevel.Canvas, Globals.currentLevel.Public_location.X);
        }
        public double rad = 0;
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
        public void Update(double x, double y)
        {
            this.X = x;
            Y = y;
        }
        public Location Copy()
        {
            return new Location(X, Y);
        }
    }
    public class MouseHandler
    {
        public MouseButtonState Pressed = MouseButtonState.Released;
        public MouseButton Button;
    }
}
