using MovingEngine.classes;
using MovingEngine.levels;
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

        public void loop()
        {
            Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
            Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            if (Globals.currentLevel.enemies.Count == 0)
            {
                rungame(1);
            }

            Moving();
            Mouse_Action();
            foreach (Projectile projectile in Globals.projectiles.ToArray())  projectile.Move();
            foreach (Enemy enemy in Globals.currentLevel.enemies.ToArray()) enemy.AI();
            Collision.CheckCollision();
            Weapons.Aim();
            Globals.Debug.Content = "Current Pos: [" + Canvas.GetLeft(Globals.currentLevel.canvas) + " | " + Canvas.GetTop(Globals.currentLevel.canvas) + "], " + Globals.player.rad + "°";
            Globals.Debug.Content += "\nVPR: " + Collision.visualPointsR[0] + ", " + Collision.visualPointsR[1] + ", " + Collision.visualPointsR[2] + ", " + Collision.visualPointsR[3];
            Globals.step = Globals.default_step;
        }


        public static void rungame(int i)
        {
            if (i == 1)
            {
                Globals.player.currentStage++;
                Globals.canvas.Children.Remove(Globals.currentLevel.canvas);
            } else if (i == 2) {
                Globals.player.currentStage = 1;
                Globals.canvas.Children.Remove(Globals.currentLevel.canvas);
            }
            Globals.currentLevel = null;
            Globals.gamelooptimer.Stop();
            Globals.player.HP = Globals.player.baseHP;
            new Level();
            if (i != 0) MainMenu.BetweenLevels(i);
        }

        private void Mouse_Action()
        {
            if (Globals.MouseHandler.Pressed == MouseButtonState.Pressed)
            {
                switch(Globals.MouseHandler.Button)
                {
                    case MouseButton.Left:
                        if (Globals.player.ShootDelayCurrent == 0)
                        {
                            Weapons.Shoot();
                            Globals.player.ShootDelayCurrent = Weapons.equipped.cooldown;
                        }
                        break;

                }
            }
            
            if (Globals.player.ShootDelayCurrent != 0) Globals.player.ShootDelayCurrent--;
        }

        private void Moving()
        {
            Key[] keys = new[] { Key.W, Key.S, Key.A, Key.D, Key.F3, Key.P};
            foreach (Key key in keys)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Key.W:
                            Globals.currentLevel.Public_location.Y += Globals.step;
                            break;

                        case Key.S:
                            Globals.currentLevel.Public_location.Y -= Globals.step;
                            break;

                        case Key.A:
                            Globals.currentLevel.Public_location.X += Globals.step;
                            break;
                        case Key.D:
                            Globals.currentLevel.Public_location.X -= Globals.step;
                            break;
                        case Key.F3:
                            if (!Globals.canvas.Children.Contains(Globals.Debug))Globals.canvas.Children.Add(Globals.Debug);
                            break;
                        case Key.P:
                            Debug.WriteLine("PAUSE");
                            break;
                    }
                }
            }
            Globals.player.UpdatePosition();
        }

        public static void Mouse(object sender, MouseButtonEventArgs e)
        {
            Globals.MouseHandler.Pressed = e.ButtonState;
            Globals.MouseHandler.Button = e.ChangedButton;
        }
    }
}
