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
        public static WeaponItem equipped = (WeaponItem)ItemBase.items.First(x => x.name == "Pass-Through Rifle");
        public static void Aim()
        {
            Globals.player.rad = Mathfuncs.XYToDegrees((Globals.mouse_position.X - Globals.Middlepoint.X), (Globals.mouse_position.Y - Globals.Middlepoint.Y));
            RotateTransform rotateTransform = new RotateTransform(Globals.player.rad, Globals.player.visual.Height / 2, Globals.player.visual.Height / 2);
            Globals.player.visual.RenderTransform = rotateTransform;
        }

        public static void Shoot()
        {
            Rectangle shoot_projectile = new Rectangle {
                Fill = equipped.projectileBrush,
                Width = equipped.projectileWidth,
                Height = equipped.projectileLength
            };

            shoot_projectile.RenderTransform = new RotateTransform(Globals.player.rad, shoot_projectile.Width / 2, shoot_projectile.Height / 2);
            double vec_x = Globals.mouse_position.X - Globals.Middlepoint.X;
            double vec_y = Globals.mouse_position.Y - Globals.Middlepoint.Y;
            double per_x = (Math.Abs(vec_x) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
            double per_y = (Math.Abs(vec_y) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
            double new_x = Math.Sqrt(Math.Pow(equipped.bulletspeed * per_x, 2)) * (vec_x != 0 ? (vec_x / Math.Abs(vec_x)) : 1);
            double new_y = Math.Sqrt(Math.Pow(equipped.bulletspeed * per_y, 2)) * (vec_y != 0 ? (vec_y / Math.Abs(vec_y)) : 1);
            equipped.OnFire(new Projectile(shoot_projectile, new Point((Collision.visualPointsR[0].X + Collision.visualPointsR[3].X) / 2 - 5, (Collision.visualPointsR[0].Y + Collision.visualPointsR[3].Y) / 2 - 20), new double[] { new_x, new_y }, false, equipped.damage));
        }
    }
}
