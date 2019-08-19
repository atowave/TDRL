using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UnnamedGame.ObjectClasses;

namespace MovingEngine.classes
{
    class SpriteList
    {
        public static Dictionary<string, ImageSource> List = new Dictionary<string, ImageSource>();
        public static void Load(string folder)
        {
            foreach (string path in Directory.GetFiles(folder))
            {
                string name = path.Split('\\')[path.Split('\\').Length - 1].Split('.')[0];
                List.Add(name, new SourcedImage(path).Source);
            }
            foreach (string path in Directory.GetDirectories(folder))
            {
                Load(path);
            }
        }
    }
}
