using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.classes
{
    static class Collision
    {
        static Rectangle a;
        static Rectangle b;
        static Rectangle c;
        static Rectangle d;
        public static void InitDebug()
        {
            a = new Rectangle { Fill = Brushes.Red, Height = 16, Width = 16 };
            b = new Rectangle { Fill = Brushes.Red, Height = 16, Width = 16 };
            c = new Rectangle { Fill = Brushes.Red, Height = 16, Width = 16 };
            d = new Rectangle { Fill = Brushes.Red, Height = 16, Width = 16 };

            //Globals.canvas.Children.Add(a);
            //Globals.canvas.Children.Add(b);
            //Globals.canvas.Children.Add(c);
            //Globals.canvas.Children.Add(d);
        }
        public static Location[] visualPointsR = { new Location(0, 0), new Location(0, 0), new Location(0, 0), new Location(0, 0) };
        public static void CheckCollision()
        {
            Location[] visualPoints = GetVisualPoints();
            visualPointsR = convertToRelative(visualPoints);
            Walls(visualPointsR);
            UpdateDebug(visualPoints);

            if (Colliding(visualPointsR))
            {
                Globals.player.UpdatePosition(Globals.player.lastLocation);
            }
            if(Enemys(visualPointsR))
            {
                Globals.player.HP--;
                Globals.DmgInd(1, Globals.player.HP, false, new Location(Globals.Middlepoint.X - Globals.currentLevel.Public_location.X, Globals.Middlepoint.Y - Globals.currentLevel.Public_location.Y - Globals.player.Height / 1));
            }
        }
        public static Location[] convertToRelative(Location[] visualPoints)
        {
            return visualPoints.Select(x => new Location(x.X - Globals.currentLevel.Public_location.X, x.Y - Globals.currentLevel.Public_location.Y)).ToArray();
        } 
        public static bool Colliding(Location[] visualPoints)
        {
            return Walls(visualPoints) || Objects(visualPoints);
        }
        public static bool Walls(Location[] visualPoints)
        {
            return (visualPoints.Any(x => x.X < 0 || x.Y < 0 || x.X > Globals.currentLevel.Lvlsize_public[0] || x.Y > Globals.currentLevel.Lvlsize_public[1]));
        }
        public static bool Enemys(Location[] visualPoints)
        {
            return visualPoints.Any(x => Globals.currentLevel.enemies.Any(y => Math.Sqrt(Math.Pow(Math.Abs(x.X) - Math.Abs(y.location.X), 2) + Math.Pow(Math.Abs(x.Y) - Math.Abs(y.location.Y), 2)) < y.hitboxRadius));
        }
        public static Enemy GetEnemy(Location[] visualPoints)
        {
            Location l = visualPoints.First(x => Globals.currentLevel.enemies.Any(y => Math.Sqrt(Math.Pow(Math.Abs(x.X) - Math.Abs(y.location.X), 2) + Math.Pow(Math.Abs(x.Y) - Math.Abs(y.location.Y), 2)) < y.hitboxRadius));
            return Globals.currentLevel.enemies.First(y => Math.Sqrt(Math.Pow(Math.Abs(l.X) - Math.Abs(y.location.X), 2) + Math.Pow(Math.Abs(l.Y) - Math.Abs(y.location.Y), 2)) < y.hitboxRadius);
        }
        public static LevelObj GetObject(Location[] visualPoints)
        {
            Location l = visualPoints.First(x => Globals.currentLevel.objs.Any(y =>
            {
                return (x.X < (y.location.X + y.size.Width) && x.Y < (y.location.Y + y.size.Height)) &&
                ((x.X) > y.location.X) && ((x.Y) > y.location.Y);
            }));
            return Globals.currentLevel.objs.First(y => (l.X < (y.location.X + y.size.Width) && l.Y < (y.location.Y + y.size.Height)) &&
                ((l.X) > y.location.X) && ((l.Y) > y.location.Y));
        }
        public static bool Objects(Location[] visualPoints)
        {
            return visualPoints.Any(x => Globals.currentLevel.objs.Any(y =>
            {
                return (x.X < (y.location.X + y.size.Width) && x.Y < (y.location.Y + y.size.Height)) &&
                ((x.X) > y.location.X) && ((x.Y) > y.location.Y);
            }));
        }
        public static void UpdateDebug(Location[] visualPoints)
        {
            Canvas.SetLeft(a, visualPoints[0].X - 8);
            Canvas.SetTop(a, visualPoints[0].Y - 8);

            Canvas.SetLeft(b, visualPoints[1].X - 8);
            Canvas.SetTop(b, visualPoints[1].Y - 8);

            Canvas.SetLeft(c, visualPoints[2].X - 8);
            Canvas.SetTop(c, visualPoints[2].Y - 8);

            Canvas.SetLeft(d, visualPoints[3].X - 8);
            Canvas.SetTop(d, visualPoints[3].Y - 8);
        }
        public static Location[] GetVisualPoints()
        {
            Location bBR = new Location(Globals.Middlepoint.X + Globals.player.Height / 2, Globals.Middlepoint.Y + Globals.player.Height / 2);
            Location bTR = new Location(Globals.Middlepoint.X + Globals.player.Height / 2, Globals.Middlepoint.Y - Globals.player.Height / 2);
            Location bBL = new Location(Globals.Middlepoint.X - Globals.player.Height / 2, Globals.Middlepoint.Y + Globals.player.Height / 2);
            Location bTL = new Location(Globals.Middlepoint.X - Globals.player.Height / 2, Globals.Middlepoint.Y - Globals.player.Height / 2);

            Tuple<double, double> nBR = Mathfuncs.RotateAroundOrigin(bBR.ToTuple(), Globals.Middlepoint.ToLocation().ToTuple(), Globals.player.rad);
            Tuple<double, double> nTR = Mathfuncs.RotateAroundOrigin(bTR.ToTuple(), Globals.Middlepoint.ToLocation().ToTuple(), Globals.player.rad);
            Tuple<double, double> nBL = Mathfuncs.RotateAroundOrigin(bBL.ToTuple(), Globals.Middlepoint.ToLocation().ToTuple(), Globals.player.rad);
            Tuple<double, double> nTL = Mathfuncs.RotateAroundOrigin(bTL.ToTuple(), Globals.Middlepoint.ToLocation().ToTuple(), Globals.player.rad);

            return new Tuple<double, double>[] { nBR, nTR, nBL, nTL }.Select(x => x.ToLocation()).ToArray();
        }
    }
}
