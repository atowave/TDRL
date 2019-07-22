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
    static class Weapons
    {
        public static void Aim()
        {
            Globals.player.rad = XYToDegrees((Globals.mouse_position.X - Globals.Middlepoint.X), (Globals.mouse_position.Y - Globals.Middlepoint.Y));
            RotateTransform rotateTransform = new RotateTransform(Globals.player.rad, Globals.player.visual.Height / 2, Globals.player.visual.Height / 2);
            Globals.player.visual.RenderTransform = rotateTransform;
        }

        public static void Shoot()
        {
            Canvas shoot_projectile = new Canvas {
                Background = Brushes.Red,
                Width = 10,
                Height = 80
            };

            shoot_projectile.RenderTransform = new RotateTransform(Globals.player.rad, 5, 0);
            double vec_x = Globals.mouse_position.X - Globals.Middlepoint.X;
            double vec_y = Globals.mouse_position.Y - Globals.Middlepoint.Y;
            double per_x = (vec_x / (vec_x + vec_y));
            double per_y = (vec_y / (vec_x + vec_y));
            double new_x = Math.Sqrt(Math.Pow(10, 2) * per_x);
            double new_y = Math.Sqrt(Math.Pow(10, 2) * per_y);
            Debug.WriteLine(vec_x + " | " + vec_y);
            new Projectile(shoot_projectile, new Point(Globals.Middlepoint.X - 5, Globals.Middlepoint.Y - 5), new double[] { vec_x, vec_y, });
        }
        public static double XYToDegrees(double x, double y)
        {
            double ox = x;
            double oy = y;
            int plus = 0;
            y *= -1;
            if (x < 0)
            {
                plus = -90;
            }
            if (y < 0)
            {
                plus = 90;
            }
            if (x < 0 && y < 0)
            {
                plus = 180;
            }
            x = Math.Abs(x);
            y = Math.Abs(y);
            if (ox == 0 && oy == 0)
            {
                return 0;
            }
            if (ox == 0)
            {
                if (oy < 0)
                {
                    return 0;
                }
                else
                {
                    return 180;
                }
            }
            if (oy == 0)
            {
                if (ox > 0)
                {
                    return 90;
                }
                else
                {
                    return 270;
                }
            }
            double deg = Math.Asin(y / Math.Sqrt(x * x + y * y)) * (180 / Math.PI);
            if (ox > 0 != oy > 0)
            {
                deg = 90 - deg;
            }
            return deg + plus;
        }
    }
}
