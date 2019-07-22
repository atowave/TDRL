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
            Globals.canvas.Children.Remove(radius);
            radius = new Polyline();
            radius.Points.Add(new Point(Globals.Middlepoint.X, Globals.Middlepoint.Y));
            radius.Points.Add(new Point(Globals.mouse_position.X, Globals.mouse_position.Y));
            radius.Stroke = Brushes.Blue;
            radius.StrokeThickness = 2;
            Globals.canvas.Children.Add(radius);

            double line = Math.Sqrt(Math.Pow(Globals.Middlepoint.X - Globals.mouse_position.X, 2) + Math.Pow(Globals.Middlepoint.Y - Globals.mouse_position.Y, 2));
            double gegen = Math.Sqrt(Math.Pow(Globals.Middlepoint.X - Globals.mouse_position.X, 2) + Math.Pow(line - Globals.mouse_position.Y, 2));
            double rad = Math.Sin(gegen / line) * 90 / Math.PI;

            Console.WriteLine(gegen);
            RotateTransform rotateTransform = new RotateTransform(rad, 45, 45);
            Globals.player.visual.RenderTransform = rotateTransform;
        }
    }
}
