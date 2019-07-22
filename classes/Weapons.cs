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
            Globals.player.rad = Mathfuncs.XYToDegrees((Globals.mouse_position.X - Globals.Middlepoint.X), (Globals.mouse_position.Y - Globals.Middlepoint.Y));
            RotateTransform rotateTransform = new RotateTransform(Globals.player.rad, Globals.player.visual.Height / 2, Globals.player.visual.Height / 2);
            Globals.player.visual.RenderTransform = rotateTransform;
        }

        public static void Shoot()
        {
            Rectangle shoot_projectile = new Rectangle {
                Fill = Brushes.Red,
                Width = 10,
                Height = 80
            };

            shoot_projectile.RenderTransform = new RotateTransform(Globals.player.rad, 5, 0);
            double vec_x = Globals.mouse_position.X - Globals.Middlepoint.X;
            double vec_y = Globals.mouse_position.Y - Globals.Middlepoint.Y;
            double per_x = (Math.Abs(vec_x) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
            double per_y = (Math.Abs(vec_y) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
            double new_x = Math.Sqrt(Math.Pow(10 * per_x, 2)) * (vec_x != 0 ? (vec_x / Math.Abs(vec_x)) : 1);
            double new_y = Math.Sqrt(Math.Pow(10 * per_y, 2)) * (vec_y != 0 ? (vec_y / Math.Abs(vec_y)) : 1);
            //double new_x = 10 * per_x * (vec_x != 0 ? (vec_x / Math.Abs(vec_x)) : 1);
            //double new_y = 10 * per_y * (vec_y != 0 ? (vec_y / Math.Abs(vec_y)) : 1);
            Debug.WriteLine(vec_x + " | " + vec_y);
            Debug.WriteLine(per_x + " | " + per_y);
            Debug.WriteLine(new_x + " | " + new_y);
            new Projectile(shoot_projectile, new Point(Globals.Middlepoint.X - 5, Globals.Middlepoint.Y - 5), new double[] { new_x, new_y, });
        }
    }
}
