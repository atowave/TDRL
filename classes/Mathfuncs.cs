using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.classes
{
    class Mathfuncs
    {
        public static Tuple<double, double> RotateAroundOrigin(Tuple<double, double> point, Tuple<double, double> origin, double degrees)
        {
            double angleInRadians = degrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Tuple<double, double>(
                    (int)
                    (cosTheta * (point.Item1 - origin.Item1) -
                    sinTheta * (point.Item2 - origin.Item2) + origin.Item1),
                    (int)
                    (sinTheta * (point.Item1 - origin.Item1) +
                    cosTheta * (point.Item2 - origin.Item2) + origin.Item2)
            );
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
                } else
                {
                    return 180;
                }
            }
            if (oy == 0)
            {
                if (ox > 0)
                {
                    return 90;
                } else
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
