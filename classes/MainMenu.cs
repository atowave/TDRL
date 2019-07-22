using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MovingEngine.classes
{
    static class MainMenu
    {
        static Canvas Menu;
        public static void Start()
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(150, 255,255,255)}, Height = Globals.canvas.Height, Width = Globals.canvas.Width };
            Canvas.SetZIndex(Menu, 3);

            Button Start = new Button
            {
                Content = "START",
                FontSize = 50 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.Yellow
            };
            Start.Click += BeginRound;
            Menu.Children.Add(Start);
            Globals.canvas.Children.Add(Menu);
            Canvas.SetTop(Menu, 20);
            Canvas.SetLeft(Menu, 10);
        }

        private static void BeginRound(object sender, RoutedEventArgs e)
        {
            Globals.canvas.Children.Remove(Menu);
            Globals.gamelooptimer.Start();
        }
    }
}