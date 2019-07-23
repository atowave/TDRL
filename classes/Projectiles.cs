using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class Projectile
    {
        Rectangle canvas;
        double[] Movement = new double[] { 0.0, 0.0 };
        Point pos = new Point(0, 0);
        public Projectile(Rectangle cn, Point origin) {
            canvas = cn;

            Globals.canvas.Children.Add(cn);
            Canvas.SetLeft(cn, origin.X);
            Canvas.SetTop(cn, origin.Y);
            pos = origin;

            Globals.projectiles.Add(this);
            Movement = new double[] { 1, 1 };
        }
        public Projectile(Rectangle cn, Point origin, double[] Movement1)
        {
            canvas = cn;

            Globals.currentLevel.Canvas.Children.Add(cn);
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

            Location center = new Location(Canvas.GetLeft(canvas) + (canvas.Width / 2), Canvas.GetTop(canvas) + (canvas.Height / 2));
            if (Collision.Colliding(new[] { center }))
            {
                Globals.projectiles.Remove(this);
                Globals.currentLevel.Canvas.Children.Remove(canvas);
            }
        }
    }
}
