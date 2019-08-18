using MovingEngine.levels;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MovingEngine.classes
{
    static class MainMenu
    {
        static Canvas Menu;
        public static void Start()
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
            Canvas.SetZIndex(Menu, 10);
            Menu.KeyDown += Menu_KeyDown;

            Button Start = new Button
            {
                Content = "START STAGE "+Globals.player.currentStage,
                FontSize = 100 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.Yellow
            };
            Button Options = new Button
            {
                Content = "OPTIONS",
                FontSize = 50 * Globals.fontSizeMultiplier,
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
            Menu.Children.Add(Options);
            Canvas.SetBottom(copyright, 0);
            Canvas.SetRight(copyright, 0);

            Globals.canvas.Children.Add(Menu);

            DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.001) };
            dt.Tick += (object sender, EventArgs e) =>
            {
                dt.Stop();
                Canvas.SetTop(Start, (Menu.Height - Start.ActualHeight) / 2);
                Canvas.SetLeft(Start, (Menu.Width - Start.ActualWidth) / 2);
                Canvas.SetTop(Options, (Menu.Height - Start.ActualHeight) / 2 + Start.ActualHeight + 10);
                Canvas.SetLeft(Options, (Menu.Width - Options.ActualWidth) / 2);
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
        static ItemBase item;

        internal static void LootMenu()
        {
            Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
            Canvas.SetZIndex(Menu, 10);
            Globals.canvas.Children.Add(Menu);
            item = ItemBase.GetRandomItem();
            Label Text = new Label
            {
                Content = "You just found a " + item.name.ToUpper() + "!\n\nDo you want to equip it? It will override a currently equipped item.",
                Foreground = Brushes.Yellow,
                FontSize = 50 * Globals.fontSizeMultiplier,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = Globals.canvas.ActualWidth
            };
            Canvas.SetTop(Text, Globals.canvas.ActualHeight * 0.2);
            Menu.Children.Add(Text);
            Button YES = new Button
            {
                Content = "YES",
                FontSize = 100 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.LightGreen
            };
            Button NO = new Button
            {
                Content = "NO",
                FontSize = 100 * Globals.fontSizeMultiplier,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Foreground = Brushes.IndianRed
            };
            Canvas.SetLeft(YES, Globals.canvas.ActualWidth * 0.35);
            Canvas.SetRight(NO, Globals.canvas.ActualWidth * 0.35);
            Canvas.SetTop(YES, Globals.canvas.ActualHeight * 0.7);
            Canvas.SetTop(NO, Globals.canvas.ActualHeight * 0.7);

            Menu.Children.Add(YES);
            Menu.Children.Add(NO);

            YES.Click += YES_Click;
            NO.Click += NO_Click;
        }

        private static void NO_Click(object sender, RoutedEventArgs e)
        {
            Globals.canvas.Children.Remove(Menu);
            BetweenLevels(1);
        }

        private static void YES_Click(object sender, RoutedEventArgs e)
        {
            item.OnItemEquip();
            Globals.canvas.Children.Remove(Menu);
            BetweenLevels(1);
        }

        internal static void BetweenLevels(int i)
        {
            Rectangle x = new Rectangle { Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth, Fill = Brushes.Black, Opacity = 0 };
            Canvas.SetTop(x, 0);
            Canvas.SetLeft(x, 0);
            Canvas.SetZIndex(x, 8);
            Globals.canvas.Children.Add(x);
            DoubleAnimation op = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.5) };
            op.Completed += (a, b) =>
            {
                DoubleAnimation op2 = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(0.5) };
                op2.Completed += (c, d) =>
                {
                    Globals.canvas.Children.Remove(x);
                };
                x.BeginAnimation(Rectangle.OpacityProperty, op2);
                Menu = new Canvas { Background = new SolidColorBrush { Color = Color.FromArgb(127, 0, 0, 0) }, Height = Globals.canvas.ActualHeight, Width = Globals.canvas.ActualWidth };
                Canvas.SetZIndex(Menu, 10);
                Menu.KeyDown += Menu_KeyDown;
                Globals.canvas.Children.Add(Menu);
                Label Stage = new Label()
                {
                    Content = (i == 1 ? "Stage " + ((int)Globals.player.currentStage - 1) + " cleared" : "Stage failed!"),
                    Foreground = (i == 1 ? Brushes.Green : Brushes.Red),
                    FontSize = 100 * Globals.fontSizeMultiplier,
                };
                Button Start = new Button
                {
                    Content = "START STAGE " + Globals.player.currentStage,
                    FontSize = 100 * Globals.fontSizeMultiplier,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    Foreground = Brushes.Yellow
                };
                Button Options = new Button
                {
                    Content = "OPTIONS",
                    FontSize = 50 * Globals.fontSizeMultiplier,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    Foreground = Brushes.Yellow
                };
                Start.Click += BeginRound;

                Menu.Children.Add(Options);
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
                    Canvas.SetTop(Options, (Menu.Height - Start.ActualHeight) / 2 + Start.ActualHeight + 10);
                    Canvas.SetLeft(Options, -Start.ActualWidth);

                    DispatcherTimer dt2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };
                    dt2.Tick += (f, g) =>
                    {
                        dt2.Stop();
                        DoubleAnimation da1 = new DoubleAnimation { From = (Menu.Width - Stage.ActualWidth) / 2, To = Menu.Width, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
                        DoubleAnimation da2 = new DoubleAnimation { From = -Start.ActualWidth, To = (Menu.Width - Start.ActualWidth) / 2, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
                        DoubleAnimation da3 = new DoubleAnimation { From = -Start.ActualWidth, To = (Menu.Width - Options.ActualWidth) / 2, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
                        Stage.BeginAnimation(Canvas.LeftProperty, da1);
                        Start.BeginAnimation(Canvas.LeftProperty, da2);
                        Options.BeginAnimation(Canvas.LeftProperty, da3);
                    };
                    dt2.Start();

                };
                dt.Start();
            };
            x.BeginAnimation(Rectangle.OpacityProperty, op);
        }
    }
}