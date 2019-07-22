using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MovingEngine.classes
{
    public class Projectile
    {
        Canvas canvas;
        double[] Movement = new double[] { 0.0, 0.0 };
        Point pos = new Point(0, 0);
        public Projectile(Canvas cn, Point origin) {
            canvas = cn;

            Globals.canvas.Children.Add(cn);
            Canvas.SetLeft(cn, origin.X);
            Canvas.SetTop(cn, origin.Y);
            pos = origin;

            Globals.projectiles.Add(this);
            Movement = new double[] { 1, 1 };
        }
        public Projectile(Canvas cn, Point origin, double[] Movement1)
        {
            canvas = cn;

            Globals.canvas.Children.Add(cn);
            Canvas.SetLeft(cn, origin.X);
            Canvas.SetTop(cn, origin.Y);
            pos = origin;

            Globals.projectiles.Add(this);
            Movement = Movement1;
        }

        public void Move()
        {
            pos.X = pos.X + Movement[0];
            pos.Y = pos.Y + Movement[1];
            Canvas.SetLeft(canvas, pos.X);
            Canvas.SetTop(canvas, pos.Y);
        }
    }
}
