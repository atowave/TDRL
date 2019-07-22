using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MovingEngine.levels
{
    class level1: Baselevel
    {
        public level1()
        {
            lvlsize = new double[] { 1500, 600 };
        }

        public override void Build_Level()
        {
            LevelGrid grid = new LevelGrid();
            grid.SetContentPos(new LevelContent(Brushes.Black), 2, 2);
        }
    }
}
