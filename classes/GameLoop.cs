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
            if (Globals.currentLevel.enemies.Count == 0 && !Globals.debugging) rungame(1);

            Weapons.Aim();
            Moving();
            Mouse_Action();
            foreach (Projectile projectile in Globals.projectiles.ToArray())  projectile.Move();
            foreach (Enemy enemy in Globals.currentLevel.enemies.ToArray()) enemy.AI();
            Collision.CheckCollision();
            Globals.Debug.Content = "Current Pos: [" + Canvas.GetLeft(Globals.currentLevel.canvas) + " | " + Canvas.GetTop(Globals.currentLevel.canvas) + "], MovVec: ["+Globals.player.Movement[0]+" | "+ Globals.player.Movement[1]+"] " + Globals.player.rad + "°"+ " Pjc:"+Globals.projectiles.Count;
            Globals.Debug.Content += "\nVPR: " + Collision.visualPointsR[0] + ", " + Collision.visualPointsR[1] + ", " + Collision.visualPointsR[2] + ", " + Collision.visualPointsR[3];
            Globals.Debug.Content += "\nWeapon: Acc " + Weapons.equipped.currentAccuracy;
            Canvas.SetZIndex(Globals.Debug, 600);
            Globals.hpbar.Width = Globals.hpbarlength * ((double)Globals.player.HP / Globals.player.baseHP);
            Globals.hptext.Content = Globals.player.HP + " / " + Globals.player.baseHP;
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
                Weapons.equipped = (WeaponItem)ItemBase.items.First(x => x.name == "Default Gun");
                Weapons.equippedEffect = (EffectItem)ItemBase.items.First(x => x.name == "No Effect");
                Globals.canvas.Children.Remove(Globals.currentLevel.canvas);
            }
            Globals.currentLevel = null;
            Globals.gamelooptimer.Stop();
            Globals.player.HP = Globals.player.baseHP;
            new Level();
            if (i == 1 && ((Globals.player.currentStage - 1) % 5 == 0) && Globals.player.currentStage != 1)
            {
                MainMenu.LootMenu();
            } else
            {
                if (i != 0)
                    MainMenu.BetweenLevels(i);
            }
        }

        public void Mouse_Action()
        {
            foreach(MouseButton mb in Globals.MouseHandler)
            {
                switch(mb)
                {
                    case MouseButton.Left:
                        if (Globals.player.ShootDelayCurrent == 0)
                        {
                            Weapons.Shoot();
                            Globals.player.ShootDelayCurrent = Weapons.equipped.cooldown;
                        }
                        break;
                    case MouseButton.Right:
                        Weapons.Zooming();
                        break;
                }
            }
            
            if (Globals.player.ShootDelayCurrent != 0) Globals.player.ShootDelayCurrent--;
        }

        public void Moving()
        {
            if (Globals.player.Movement[0] > 0)
            {
                if (Globals.player.Movement[0] < Globals.currentLevel.Slithering)
                {
                    Globals.player.Movement[0] = 0;
                } else
                {
                    Globals.player.Movement[0] -= Globals.currentLevel.Slithering;
                }
            } else if(Globals.player.Movement[0] < 0)
            {
                if (Globals.player.Movement[0] > Globals.currentLevel.Slithering)
                {
                    Globals.player.Movement[0] = 0;
                }
                else
                {
                    Globals.player.Movement[0] += Globals.currentLevel.Slithering;
                }
            }
            if (Globals.player.Movement[1] > 0)
            {
                if (Globals.player.Movement[1] < Globals.currentLevel.Slithering)
                {
                    Globals.player.Movement[1] = 0;
                }
                else
                {
                    Globals.player.Movement[1] -= Globals.currentLevel.Slithering;
                }
            }
            else if (Globals.player.Movement[1] < 0)
            {
                if (Globals.player.Movement[1] > Globals.currentLevel.Slithering)
                {
                    Globals.player.Movement[1] = 0;
                }
                else
                {
                    Globals.player.Movement[1] += Globals.currentLevel.Slithering;
                }
            }
            Key[] keys = new[] { Key.W, Key.S, Key.A, Key.D, Key.F3, Key.P};
            foreach (Key key in keys)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Key.W:
                            Globals.player.Movement[1] = Globals.step;
                            break;

                        case Key.S:
                            Globals.player.Movement[1] = -Globals.step;
                            break;

                        case Key.A:
                            Globals.player.Movement[0] = Globals.step;
                            break;
                        case Key.D:
                            Globals.player.Movement[0] = -Globals.step;
                            break;
                        case Key.F3:
                            if (!Globals.canvas.Children.Contains(Globals.Debug))Globals.canvas.Children.Add(Globals.Debug);
                            Globals.debugging = true;
                            Globals.canvas.Children.Remove(Globals.currentLevel.canvas);
                            Globals.currentLevel = null;
                            Globals.canvas.Children.Remove(Globals.hpbar);
                            Globals.canvas.Children.Remove(Globals.hptext);
                            Globals.canvas.Children.Remove(Globals.HPbars[0]);
                            Globals.canvas.Children.Remove(Globals.HPbars[1]);
                            new Level(true);
                            break;
                        case Key.P:
                            Debug.WriteLine("PAUSE");
                            break;
                    }
                }
            }
            Globals.player.UpdatePosition();
        }
    }
}
