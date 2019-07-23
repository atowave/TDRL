using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class Projectile
    {
        Rectangle canvas;
        double[] Movement = new double[] { 0.0, 0.0 };
        bool enemy;
        Point pos = new Point(0, 0);
        double dmg;
        public Projectile(Rectangle cn, Point origin, double[] Movement1, bool enemy = false, double dmg = 10)
        {
            canvas = cn;
            this.enemy = enemy;
            this.dmg = dmg;
            Globals.currentLevel.Canvas.Children.Add(cn);
            Canvas.SetLeft(cn, origin.X);
            Canvas.SetTop(cn, origin.Y);
            pos = origin;

            Globals.projectiles.Add(this);
            Movement = Movement1;
        }
        public Projectile copy()
        {
            return (Projectile)this.MemberwiseClone();
        }
        public void Move()
        {
            pos.X = pos.X + Movement[0];
            pos.Y = pos.Y + Movement[1];
            Canvas.SetLeft(canvas, pos.X);
            Canvas.SetTop(canvas, pos.Y);

            Location center = new Location(Canvas.GetLeft(canvas) + (canvas.Width / 2), Canvas.GetTop(canvas) + (canvas.Height / 2));
            if (Collision.Colliding(new[] { center }))
            {
                Globals.projectiles.Remove(this);
                Globals.currentLevel.Canvas.Children.Remove(canvas);
            }
            if (Collision.Enemys(new[] { center }) && !enemy)
            {
                Enemy e = Collision.GetEnemy(new[] { center });
                e.HP -= dmg;
                Globals.DmgInd((int)dmg, (int)e.HP, true, new Location(e.location.X - 25, e.location.Y - 75));
                Globals.projectiles.Remove(this);
                Globals.currentLevel.Canvas.Children.Remove(canvas);
            }
        }
    }
}
