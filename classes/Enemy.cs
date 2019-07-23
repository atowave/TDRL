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
        public Location location;
        public FrameworkElement sprite;
        public double hitboxRadius = 40;

        public Enemy(Location origin, FrameworkElement sprite, double hitboxRadius = 40)
        {
            location = origin;
            this.sprite = sprite;
            this.hitboxRadius = hitboxRadius;

            Globals.currentLevel.Canvas.Children.Add(sprite);
            Globals.currentLevel.enemies.Add(this);
        }
    }
}
