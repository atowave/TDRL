using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UnnamedGame.ObjectClasses
{
    [Serializable]
    public class SourcedImage
    {
        public Image baseImage = new Image();
        public Image baseimg;
        public Image image;
        public ImageSource Source;

        public static Dictionary<string, BitmapImage> loadedSources = new Dictionary<string, BitmapImage>();

        public SourcedImage(string source)
        {

            Source = SourceToImage(source);
            image = new Image
            {
                Source = Source
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