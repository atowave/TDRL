using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UnnamedGame.ObjectClasses;

namespace MovingEngine.classes
{
    class SpriteList
    {
        public static Dictionary<string, ImageSource> List = new Dictionary<string, ImageSource>()
        {
            {"enemy", new SourcedImage("pack://application:,,,/resources/meshes/enemy.png").Source}
        };
    }
}
