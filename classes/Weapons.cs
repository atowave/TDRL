using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.classes
{
    static class Weapons
    {
        public static void Aim()
        {
            globals.canvas.Children.Remove(radius);
            radius = new Polyline();
            radius.Points.Add(new Point(globals.middlepoint.X, globals.middlepoint.Y));
            radius.Points.Add(new Point(globals.mouse_position.X, globals.mouse_position.Y));
            radius.Stroke = Brushes.Blue;
            radius.StrokeThickness = 2;
            globals.canvas.Children.Add(radius);

            double line = Math.Sqrt(Math.Pow(globals.middlepoint.X - globals.mouse_position.X, 2) + Math.Pow(globals.middlepoint.Y - globals.mouse_position.Y, 2));
            double gegen = Math.Sqrt(Math.Pow(globals.middlepoint.X - globals.mouse_position.X, 2) + Math.Pow(line - globals.mouse_position.Y, 2));
            double rad = Math.Sin(gegen / line) * 90 / Math.PI;

            Console.WriteLine(gegen);
            RotateTransform rotateTransform = new RotateTransform(rad, 45, 45);
            globals.player.visual.RenderTransform = rotateTransform;
        }
    }
}
