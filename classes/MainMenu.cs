using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MovingEngine.classes
{
    static class MainMenu
    {
        static Canvas Menu;
        public static void Start(MainWindow main)
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
            Canvas.SetZIndex(Menu, 3);

            Button Start = new Button
            {
                Content = "START STAGE",
                FontSize = 100 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.Yellow
            };
            Label copyright = new Label
            {
                Content = "© AtoWave",
                FontSize = 30 * Globals.fontSizeMultiplier,
                Foreground = Brushes.Yellow
            };
            Start.Click += BeginRound;

            Menu.Children.Add(Start);
            Menu.Children.Add(copyright);
            Canvas.SetBottom(copyright, 0);
            Canvas.SetRight(copyright, 0);

            Globals.canvas.Children.Add(Menu);

            DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.001) };
            dt.Tick += (object sender, EventArgs e) =>
            {
                dt.Stop();
                Canvas.SetTop(Start, (Menu.Height - Start.ActualHeight) / 2);
                Canvas.SetLeft(Start, (Menu.Width - Start.ActualWidth) / 2);

                main.Show();
            };
            dt.Start();
        }

        private static void BeginRound(object sender, RoutedEventArgs e)
        {
            Globals.canvas.Children.Remove(Menu);
            Globals.gamelooptimer.Start();
        }
    }
}