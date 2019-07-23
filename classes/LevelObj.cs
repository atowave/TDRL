using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class LevelObj
    {
        public Location location;
        public Size size;
        public Brush brush;
        public Rectangle rect;

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
            rect = new Rectangle { Fill = brush, Height = size.Height, Width = size.Width };
            Canvas.SetLeft(rect, location.X);
            Canvas.SetTop(rect, location.Y);

            Globals.currentLevel.Canvas.Children.Add(rect);
            Globals.currentLevel.objs.Add(this);
        }
    }
}
