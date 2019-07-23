using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    public class Projectile
    {
        Rectangle canvas;
        internal double[] Movement = new double[] { 0.0, 0.0 };
        bool enemy;
        Point pos = new Point(0, 0);
        double dmg;
        public Projectile(Rectangle cn, Point origin, double[] Movement1, bool enemy = false, double dmg = 10)
        {
            canvas = cn;
            this.enemy = enemy;
            this.dmg = dmg;
            Globals.currentLevel.canvas.Children.Add(cn);
            Canvas.SetLeft(cn, origin.X);
            Canvas.SetTop(cn, origin.Y);
            pos = origin;

            Globals.projectiles.Add(this);
            Movement = Movement1;
        }
        public Projectile copy()
        {
            Projectile np = (Projectile)this.MemberwiseClone();
            np.canvas = new Rectangle { Height = canvas.Height, Width = canvas.Width, Fill = canvas.Fill };
            Canvas.SetLeft(np.canvas, Canvas.GetLeft(canvas));
            Canvas.SetTop(np.canvas, Canvas.GetTop(canvas));
            Globals.currentLevel.Canvas.Children.Add(np.canvas);
            np.canvas.RenderTransform = new RotateTransform(((RotateTransform)canvas.RenderTransform).Angle, ((RotateTransform)canvas.RenderTransform).CenterX, ((RotateTransform)canvas.RenderTransform).CenterY);
            return np;
        }
        public void Move()
        {
            Canvas.SetZIndex(canvas, -1);

            pos.X = pos.X + Movement[0];
            pos.Y = pos.Y + Movement[1];
            Canvas.SetLeft(canvas, pos.X);
            Canvas.SetTop(canvas, pos.Y);

            Location center = new Location(Canvas.GetLeft(canvas) + (canvas.Width / 2), Canvas.GetTop(canvas) + (canvas.Height / 2));
            if (Collision.Colliding(new[] { center }))
            {
                Globals.projectiles.Remove(this);
                Globals.currentLevel.canvas.Children.Remove(canvas);
            }
            if (Collision.Enemys(new[] { center }) && !enemy)
            {
                Enemy e = Collision.GetEnemy(new[] { center });
                if (e.wait == 0)
                {
                    e.HP -= dmg;
                    Globals.DmgInd((int)dmg, (int)e.HP, true, new Location(e.location.X - 25, e.location.Y - 75));
                    Globals.projectiles.Remove(this);
                    Globals.currentLevel.canvas.Children.Remove(canvas);
                }
            }
            Point Middlepoint = new Point((Collision.visualPointsR[0].X + Collision.visualPointsR[3].X) / 2, (Collision.visualPointsR[0].Y + Collision.visualPointsR[3].Y) / 2);
            Location rotatedCenter = Mathfuncs.RotateAroundOrigin(center.ToTuple(), Middlepoint.ToLocation().ToTuple(), -Globals.player.rad).ToLocation();
            if (rotatedCenter.X > (Middlepoint.X - (Globals.player.Height / 2)) &&
                rotatedCenter.Y > (Middlepoint.Y - (Globals.player.Height / 2)) &&
                rotatedCenter.X < (Middlepoint.X + (Globals.player.Height / 2)) &&
                rotatedCenter.Y < (Middlepoint.Y + (Globals.player.Height / 2)) &&
                enemy)
            {
                Globals.player.HP -= (int)dmg;
                Globals.DmgInd((int)dmg, 0, false, new Location(Globals.Middlepoint.X - Globals.currentLevel.Public_location.X, Globals.Middlepoint.Y - Globals.currentLevel.Public_location.Y - Globals.player.Height / 1));
                Globals.projectiles.Remove(this);
                Globals.currentLevel.Canvas.Children.Remove(canvas);
            }
        }
    }
}
