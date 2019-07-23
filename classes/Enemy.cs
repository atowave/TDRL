﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class Enemy
    {
        public Location location
        {
            get
            {
                return new Location(Canvas.GetLeft(sprite) + (sprite.Width / 2), Canvas.GetTop(sprite) + (sprite.Height / 2));
            }
            set
            {
                Canvas.SetLeft(sprite, value.X - (sprite.Width / 2));
                Canvas.SetTop(sprite, value.Y - (sprite.Height / 2));
            }
        }
        public FrameworkElement sprite;
        public double hitboxRadius = 40;
        public double rotation = 0;
        public double AI_Threshold = 200;
        public double AI_Speed = 2.5;
        public double HP = 100;
        public double wait;

        public Enemy(Location origin, FrameworkElement sprite, double hitboxRadius = 40, double HP = 100)
        {
            this.sprite = sprite;
            this.hitboxRadius = hitboxRadius;
            this.HP = HP;

            Canvas.SetLeft(sprite, origin.X);
            Canvas.SetTop(sprite, origin.Y);
            Canvas.SetZIndex(sprite, 1);

            wait = 60;

            Globals.currentLevel.canvas.Children.Add(sprite);
            Globals.currentLevel.enemies.Add(this);
        }
        public void AI()
        {
            if (wait == 0)
            {
                Point player_pos = new Point((Collision.visualPointsR[0].X + Collision.visualPointsR[3].X) / 2, (Collision.visualPointsR[0].Y + Collision.visualPointsR[3].Y) / 2);
                double vec_x = player_pos.X - (location.X);
                double vec_y = player_pos.Y - (location.Y);
                double per_x = (Math.Abs(vec_x) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
                double per_y = (Math.Abs(vec_y) / (Math.Abs(vec_x) + Math.Abs(vec_y)));
                double new_x = Math.Sqrt(Math.Pow(AI_Speed * per_x, 2)) * (vec_x != 0 ? (vec_x / Math.Abs(vec_x)) : 1);
                double new_y = Math.Sqrt(Math.Pow(AI_Speed * per_y, 2)) * (vec_y != 0 ? (vec_y / Math.Abs(vec_y)) : 1);
                rotation = Mathfuncs.XYToDegrees(vec_x, vec_y);

                sprite.RenderTransform = new RotateTransform(rotation, sprite.ActualWidth / 2, sprite.ActualHeight / 2);

                if (Math.Sqrt(vec_x * vec_x + vec_y * vec_y) > AI_Threshold)
                {
                    Canvas.SetTop(sprite, location.Y - (sprite.ActualHeight / 2) + new_y);
                    Canvas.SetLeft(sprite, location.X - (sprite.ActualWidth / 2) + new_x);
                }

                double rand = Globals.random.Next(0, 100);
                if (rand <= 10)
                {
                    Rectangle shoot_projectile = new Rectangle
                    {
                        Fill = Brushes.Red,
                        Width = 10,
                        Height = 40
                    };
                    shoot_projectile.RenderTransform = new RotateTransform(rotation, 5, 20);

                    new_x = Math.Sqrt(Math.Pow(20 * per_x, 2)) * (vec_x != 0 ? (vec_x / Math.Abs(vec_x)) : 1);
                    new_y = Math.Sqrt(Math.Pow(20 * per_y, 2)) * (vec_y != 0 ? (vec_y / Math.Abs(vec_y)) : 1);
                    new Projectile(shoot_projectile, new Point(location.X - 5, location.Y - 20), new double[] { new_x, new_y }, true);

                }

                if (HP <= 0)
                {
                    Globals.currentLevel.canvas.Children.Remove(sprite);
                    Globals.currentLevel.enemies.Remove(this);
                }
            } else
            {
                wait--;
            }
        }
    }
}
