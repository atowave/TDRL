using MovingEngine.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine
{
    public class GameLoop
    {
        Polyline radius;
        public GameLoop()
        {
            Moving();
            Collision.Walls();
            Weapons.Aim();
            Globals.Debug.Content = "Current Top: " + Canvas.GetTop(Globals.currentLevel.canvas);
            if (!Keyboard.IsKeyDown(Key.LeftShift)) Globals.step = Globals.default_step;
        }

        private void Moving()
        {
            Key[] keys = new[] { Key.W, Key.S, Key.A, Key.D, Key.LeftShift };
            foreach (Key key in keys)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Key.W:
                            Globals.currentLevel.public_location.Y += Globals.step;
                            Globals.player.UpdatePosition();
                            break;

                        case Key.S:
                            Globals.currentLevel.public_location.Y -= Globals.step;
                            Globals.player.UpdatePosition();
                            break;

                        case Key.A:
                            Globals.currentLevel.public_location.X += Globals.step;
                            Globals.player.UpdatePosition();
                            break;
                        case Key.D:
                            Globals.currentLevel.public_location.X -= Globals.step;
                            Globals.player.UpdatePosition();
                            break;
                        case Key.LeftShift:
                            Globals.step = Globals.default_step + 10;
                            break;
                    }
                }
            }
        }
    }
}
