using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MovingEngine.classes;
using MovingEngine.levels;

namespace MovingEngine
{
    public static class Globals
    {
        public static void DmgInd(int dmg, int currenthp, bool enemy, Location location)
        {
            Label dmglabel = new Label { Content = -dmg + (enemy ? ", (" + currenthp + ")" : ""), Foreground = (enemy ? Brushes.Green : Brushes.Red), FontSize = 36 * Globals.fontSizeMultiplier };
            Canvas.SetLeft(dmglabel, location.X + random.Next(-32, 33));
            Canvas.SetTop(dmglabel, location.Y);

            Globals.currentLevel.Canvas.Children.Add(dmglabel);

            DoubleAnimation opacity = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.HoldEnd);
            DoubleAnimation ypos = new DoubleAnimation(location.Y, location.Y - 100, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.HoldEnd);
            dmglabel.BeginAnimation(Canvas.TopProperty, ypos);
            dmglabel.BeginAnimation(FrameworkElement.OpacityProperty, opacity);

            ypos.Completed += (a, b) =>
            {
                Globals.currentLevel.Canvas.Children.Remove(dmglabel);
            };
        }

        public static Canvas canvas;
        public static Player player = new Player();
        public static Baselevel currentLevel;
        public const double default_step = 7.5;
        public static double step;
        public static Point mouse_position;
        public static Random random = new Random();
        public static double fontSizeMultiplier;
        public static DispatcherTimer gamelooptimer;
        public static MouseHandler MouseHandler = new MouseHandler();
        public static List<Projectile> projectiles = new List<Projectile>();
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
        public const int baseHP = 180;
        public int HP = baseHP;
        public Location lastLocation = new Location(0, 0);
        public Canvas visual = new Canvas { Width = 70, Height = 70, Background = Brushes.Red };
        public double ShootDelay = 10;
        public double ShootDelayCurrent = 0;
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
    public class MouseHandler
    {
        public MouseButtonState Pressed = MouseButtonState.Released;
        public MouseButton Button;
    }
}
