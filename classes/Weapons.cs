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
            Globals.player.rad = Mathfuncs.XYToDegrees((Globals.mouse_position.X - Globals.Middlepoint.X), (Globals.mouse_position.Y - Globals.Middlepoint.Y));
            RotateTransform rotateTransform = new RotateTransform(Globals.player.rad, 35, 35);
            Globals.player.visual.RenderTransform = rotateTransform;
        }

        public static void Shoot()
        {

        }
    }
}
