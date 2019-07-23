using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MovingEngine.classes
{
    static class MainMenu
    {
        static Canvas Menu;
        public static void Start()
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
            Canvas.SetZIndex(Menu, 3);
            Menu.KeyDown += Menu_KeyDown;

            Button Start = new Button
            {
                Content = "START STAGE "+Globals.player.currentStage,
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

                Globals.canvas.Children.Remove(Globals.loading);
            };
            dt.Start();
        }

        private static void Menu_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Debug.WriteLine(e.Key);
            if (e.Key == System.Windows.Input.Key.Space)
            {
                Globals.canvas.Children.Remove(Menu);
                Globals.gamelooptimer.Start();
            }
        }

        private static void BeginRound(object sender, RoutedEventArgs e)
        {
            Globals.canvas.Children.Remove(Menu);
            Globals.gamelooptimer.Start();
        }

        internal static void BetweenLevels(int i)
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
            Canvas.SetZIndex(Menu, 3);
            Menu.KeyDown += Menu_KeyDown;
            Globals.canvas.Children.Add(Menu);
            Label Stage = new Label()
            {
                Content = (i == 1 ? "Stage " + ((int)Globals.player.currentStage - 1) + " cleared" : "Stage failed!"),
                Foreground = (i == 1 ? Brushes.Green : Brushes.Red),
                FontSize = 100 * Globals.fontSizeMultiplier
            };
            Button Start = new Button
            {
                Content = "START STAGE " + Globals.player.currentStage,
                FontSize = 100 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.Yellow
            };
            Start.Click += BeginRound;
            Menu.Children.Add(Stage);
            Menu.Children.Add(Start);
            DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.001) };
            dt.Tick += (object sender, EventArgs e) =>
            {
                dt.Stop();
                Canvas.SetTop(Stage, (Menu.Height - Stage.ActualHeight) / 2);
                Canvas.SetLeft(Stage, (Menu.Width - Stage.ActualWidth) / 2);

                Canvas.SetTop(Start, (Menu.Height - Start.ActualHeight) / 2);
                Canvas.SetLeft(Start, -Start.ActualWidth);

                DispatcherTimer dt2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                dt2.Tick += (a, b) =>
                {
                    dt2.Stop();
                    DoubleAnimation da1 = new DoubleAnimation { From = (Menu.Width - Stage.ActualWidth) / 2, To = Menu.Width, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
                    DoubleAnimation da2 = new DoubleAnimation { From = -Start.ActualWidth, To = (Menu.Width - Start.ActualWidth) / 2, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
                    Stage.BeginAnimation(Canvas.LeftProperty, da1);
                    Start.BeginAnimation(Canvas.LeftProperty, da2);
                };
                dt2.Start();

            };
            dt.Start();
        }
    }
}