using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class LevelObj
    {
        public Location location;
        public Size size;
        public Brush brush;
        public Canvas rect;
        public bool collision = true;
        public Action actionOnCollision = () => { };

        public LevelObj(Location location, Size size, Brush brush)
        {
            if (size.Height < Globals.player.Height)
            {
                size.Height = Globals.player.Height;
                Debug.WriteLine("Height Adjusted!");
            }
            if (size.Width < Globals.player.Height)
            {
                size.Width = Globals.player.Height;
                Debug.WriteLine("Width Adjusted!");
            }

            this.location = location;
            this.size = size;
            this.brush = brush;
            rect = new Canvas { Background = brush, Height = size.Height, Width = size.Width };
            if ((bool)Globals.settings["Graphics"]["CalculatedGlowEffectEnabled"]) rect.Effect =
                new DropShadowEffect
                {
                    Color = ((SolidColorBrush)brush).Color,
                    Direction = 0,
                    ShadowDepth = 0,
                    Opacity = 1,
                    BlurRadius = (int)Globals.settings["Graphics"]["CalculatedGlowEffectRadius"]
                };
            Canvas.SetLeft(rect, location.X);
            Canvas.SetTop(rect, location.Y);

            Globals.currentLevel.canvas.Children.Add(rect);
            Globals.currentLevel.objs.Add(this);
        }
        public void Disappear()
        {
            Globals.currentLevel.canvas.Children.Remove(rect);
            Globals.currentLevel.objs.Remove(this);
        }
    }
    public class ExitLevelObj : LevelObj
    {
        public ExitLevelObj(Location location, Size size, Brush brush) : base(location, size, brush) {
            collision = false;
            actionOnCollision = () => GameLoop.rungame(1);
            Border border = new Border
            {
                Height = rect.Height + 20,
                Width = rect.Width + 20,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(10)
            };
            rect.Children.Add(border);
            if ((bool)Globals.settings["Graphics"]["CalculatedGlowEffectEnabled"]) rect.Effect =
                new DropShadowEffect
                {
                    Color = Brushes.Black.Color,
                    Direction = 0,
                    ShadowDepth = 0,
                    Opacity = 1,
                    BlurRadius = (int)Globals.settings["Graphics"]["CalculatedGlowEffectRadius"]
                };
            Canvas.SetLeft(border, -10);
            Canvas.SetTop(border, -10);
        }
    }
}
