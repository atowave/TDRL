using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UnnamedGame.ObjectClasses
{
    [Serializable]
    public class SourcedImage
    {
        public Image baseImage = new Image();
        public Image baseimg;
        public Image image;

        public static Dictionary<string, BitmapImage> loadedSources = new Dictionary<string, BitmapImage>();

        public SourcedImage(string source)
        {
            image = new Image
            {
                Source = SourceToImage(source)
            };
        }

        public static BitmapImage SourceToImage(string src)
        {
            if (loadedSources.ContainsKey(src))
            {
                return loadedSources[src];
            } else
            {
                loadedSources.Add(src, new BitmapImage(new Uri(src)));
                return loadedSources[src];
            }
        }
    }
}