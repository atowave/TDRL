using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MovingEngine
{
    class LevelGrid
    {
        public Dictionary<string, double[]> Grid = new Dictionary<string, double[]>();
        public LevelGrid(double Size = 10)
        {
            double Columns = Globals.currentLevel.Lvlsize_public[0] / Size;
            double Rows = Globals.currentLevel.Lvlsize_public[1] / Size;

            for (int r = 0; r < Rows; r++)
            {
                for (double c = 0; c < Rows; c++)
                {
                    Grid.Add("r" + r + "c" + c, new double[] { r * Size, c * Size, r * Size + Size, c * Size + Size });
                }
            }
        }
        public void SetContentPos(LevelContent lvlcontent, double Column, double Row)
        {
            double[] postions = Grid["r" + Row + "c" + Column];
            Canvas.SetTop(lvlcontent.sprite, postions[0]);
            Canvas.SetLeft(lvlcontent.sprite, postions[1]);
            Canvas.SetZIndex(lvlcontent.sprite, 1);
            Globals.currentLevel.Canvas.Children.Add(lvlcontent.sprite);
        }
    }

    class LevelContent
    {
        public Canvas sprite = new Canvas();
        public LevelContent(Image sprite)
        {
            this.sprite.Children.Add(sprite);
        }
        public LevelContent(SolidColorBrush color)
        {
            this.sprite.Background = color;
        }
    }
}
