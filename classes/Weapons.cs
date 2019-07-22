using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    static class Weapons
    {
        public static void Aim()
        {
            Globals.player.rad = XYToDegrees((Globals.mouse_position.X - Globals.Middlepoint.X), (Globals.mouse_position.Y - Globals.Middlepoint.Y));

            Console.WriteLine(rad);
            RotateTransform rotateTransform = new RotateTransform(rad, 45, 45);
            Globals.player.visual.RenderTransform = rotateTransform;
        }

        public static 
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
