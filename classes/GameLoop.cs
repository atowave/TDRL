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
            globals.Debug.Content = "Current Top: " + Canvas.GetTop(globals.currentLevel.canvas);
            if (!Keyboard.IsKeyDown(Key.LeftShift)) globals.step = globals.default_step;
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
                            globals.currentLevel.public_location.Y += globals.step;
                            globals.player.UpdatePosition();
                            break;

                        case Key.S:
                            globals.currentLevel.public_location.Y -= globals.step;
                            globals.player.UpdatePosition();
                            break;

                        case Key.A:
                            globals.currentLevel.public_location.X += globals.step;
                            globals.player.UpdatePosition();
                            break;
                        case Key.D:
                            globals.currentLevel.public_location.X -= globals.step;
                            globals.player.UpdatePosition();
                            break;
                        case Key.LeftShift:
                            globals.step = globals.default_step + 10;
                            break;
                    }
                }
            }
        }
    }
}
