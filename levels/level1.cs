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
using UnnamedGame.ObjectClasses;
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
            new Enemy(new Location(300, 300), new Rectangle { Height = 75, Width = 75, Fill = Brushes.Blue });
        }
    }
}
