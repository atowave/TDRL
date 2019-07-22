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

namespace MovingEngine.levels
{
    public class Baselevel
    {
        public List<LevelObj> objs = new List<LevelObj>();
        protected double[] lvlsize = new double[] { 500, 500 };
        protected Canvas levelground = new Canvas { Background = Brushes.White};
        protected Location location = new Location(0, 0);
        protected Location defaultlocation = new Location(0,0);
        
        public Location Public_location { get { return location; } }
        public Canvas Canvas { get { return levelground; } }
        public Location Defaultloc { get { return defaultlocation; } }
        public double[] Lvlsize_public { get { return lvlsize; } }
        virtual public void Start()
        {
            levelground.Width = lvlsize[0];
            levelground.Height = lvlsize[1];
            location.Y = (Globals.canvas.ActualHeight - lvlsize[1]) / 2;
            location.X = (Globals.canvas.ActualWidth - lvlsize[0]) / 2;
            defaultlocation = location.Copy(); 
            Canvas.SetTop(levelground, location.Y);
            Canvas.SetLeft(levelground, location.X);
            Globals.canvas.Children.Add(levelground);

            Globals.currentLevel = this;
        }
        virtual public void Build_Level()
        {
            return;
        }
    }
}
