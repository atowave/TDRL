using MovingEngine.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.levels
{
    public class Level
    {
        public List<Enemy> enemies = new List<Enemy>();
        public List<LevelObj> objs = new List<LevelObj>();

        public Location Public_location { get; }
        public Canvas canvas { get; }
        public double[] Lvlsize_public { get; }
        public Level()
        {
            Globals.currentLevel = this;
            Lvlsize_public = new double[] {
                (Globals.canvas.ActualWidth /100) *80,
                (Globals.canvas.ActualHeight /100) *80,
                
            };
            canvas = new Canvas { Background = Brushes.White, Width = Lvlsize_public[0], Height = Lvlsize_public[1] };
            Public_location = new Location((Globals.canvas.ActualWidth - Lvlsize_public[0]) / 2, (Globals.canvas.ActualHeight - Lvlsize_public[1]) / 2);
            Globals.canvas.Children.Add(canvas);
            Canvas.SetLeft(canvas, (Globals.canvas.ActualWidth - Lvlsize_public[0]) / 2);
            Canvas.SetTop(canvas, (Globals.canvas.ActualHeight - Lvlsize_public[1]) / 2);

            for(int range = 0; range < Globals.random.Next(5, 15); range++)
            {
                int rand = Globals.random.Next(0, 100);
                if (rand < 50) {
                    new Enemy(new Location(Globals.random.Next(0, (int)Lvlsize_public[0]), Globals.random.Next(0, (int)Lvlsize_public[1])), new Rectangle { Fill = Brushes.Red, Width=Lvlsize_public[0]/100*5, Height= Lvlsize_public[0] / 100 * 5 }, 40, Globals.random.Next(100, 1000));
                } else
                {
                    double size_x = Lvlsize_public[0] / 100 * Globals.random.Next(2, 10);
                    double size_y = Lvlsize_public[1] / 100 * Globals.random.Next(5, 10);
                    Size size = new Size((size_x < Globals.player.visual.Width ? Globals.player.visual.Width : size_x), (size_y < Globals.player.visual.Height ? Globals.player.visual.Height : size_y));
                    new LevelObj(new Location(Globals.random.Next(0, (int)Lvlsize_public[0]), Globals.random.Next(0, (int)Lvlsize_public[1])), size, Brushes.Black);
                }
            }
        }
    }
}
