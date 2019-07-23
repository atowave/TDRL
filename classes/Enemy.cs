using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        public double HP = 100;

        public Enemy(Location origin, FrameworkElement sprite, double hitboxRadius = 40, double HP = 100)
        {
            this.sprite = sprite;
            this.hitboxRadius = hitboxRadius;
            this.HP = HP;

            Canvas.SetLeft(sprite, origin.X);
            Canvas.SetTop(sprite, origin.Y);

            Globals.currentLevel.Canvas.Children.Add(sprite);
            Globals.currentLevel.enemies.Add(this);
        }
    }
}
