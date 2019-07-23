using MovingEngine.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MovingEngine.levels
{
    class Level1: Baselevel
    {
        public Level1()
        {
            lvlsize = new double[] { 1500, 600 };
        }

        public override void Build_Level()
        {
            LevelGrid grid = new LevelGrid();
            grid.SetContentPos(new LevelContent(Brushes.Black), 2, 2);
            new LevelObj(new Location(150, 150), new Size(40, 40), Brushes.Red);
            new Enemy(new Location(300, 300), new Rectangle { Height = 75, Width = 75, Fill = Brushes.Blue });
        }
    }
}
