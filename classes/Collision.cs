using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovingEngine.classes
{
    static class Collision
    {
        public static void Walls()
        {
            if (Canvas.GetTop(globals.currentLevel.canvas) >= Canvas.GetTop(globals.player.visual) && Keyboard.IsKeyDown(Key.W)) globals.currentLevel.public_location.Y = Canvas.GetTop(globals.player.visual);
            if (Canvas.GetLeft(globals.currentLevel.canvas) >= Canvas.GetLeft(globals.player.visual) && Keyboard.IsKeyDown(Key.A)) globals.currentLevel.public_location.X = Canvas.GetLeft(globals.player.visual);
            if (Canvas.GetTop(globals.currentLevel.canvas) + globals.currentLevel.canvas.ActualHeight <= Canvas.GetTop(globals.player.visual) + globals.player.height && Keyboard.IsKeyDown(Key.S)) globals.currentLevel.public_location.Y = Canvas.GetTop(globals.player.visual) + globals.player.visual.ActualHeight - globals.currentLevel.canvas.ActualHeight;
            if (Canvas.GetLeft(globals.currentLevel.canvas) + globals.currentLevel.canvas.ActualWidth <= Canvas.GetLeft(globals.player.visual) + globals.player.visual.ActualWidth && Keyboard.IsKeyDown(Key.D)) globals.currentLevel.public_location.X = Canvas.GetLeft(globals.player.visual) + globals.player.visual.ActualWidth - globals.currentLevel.canvas.ActualWidth;
        }
    }
}
