using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        public double AI_Threshold = 100;
        public double HP = 100;

        public Enemy(Location origin, FrameworkElement sprite, double hitboxRadius = 40, double HP = 100)
        {
            this.sprite = sprite;
            this.hitboxRadius = hitboxRadius;
            this.HP = HP;

            Canvas.SetLeft(sprite, origin.X);
            Canvas.SetTop(sprite, origin.Y);

            Canvas.SetTop(sprite, origin.X);
            Canvas.SetLeft(sprite, origin.Y);

            Globals.currentLevel.Canvas.Children.Add(sprite);
            Globals.currentLevel.enemies.Add(this);
        }
        public void AI()
        {
            Point player_pos = new Point((Collision.visualPointsR[0].X + Collision.visualPointsR[3].X) / 2, (Collision.visualPointsR[0].Y + Collision.visualPointsR[3].Y) / 2);
            double vec_x = player_pos.X - (location.X + (sprite.ActualWidth / 2));
            double vec_y = player_pos.Y - (location.Y + (sprite.ActualHeight / 2));
            rotation = Mathfuncs.XYToDegrees(vec_x, vec_y);
            Debug.WriteLine("PlayerPos: "+player_pos);
            Debug.WriteLine("Vec X: "+vec_x);
            Debug.WriteLine("Vec Y: "+vec_y);
            Debug.WriteLine("Rot: " + rotation);

            sprite.RenderTransform = new RotateTransform(rotation, sprite.ActualWidth / 2, sprite.ActualHeight / 2);

            if (Math.Sqrt(vec_x * vec_x + vec_y * vec_y) > AI_Threshold)
            {
                Canvas.SetTop(sprite, location.Y + vec_y);
                Canvas.SetLeft(sprite, location.X + vec_x);
                location.Y += vec_y;
                location.X += vec_x;
            }
        }
    }
}
