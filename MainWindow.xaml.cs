using Ied.Extentions;
using MovingEngine.classes;
using MovingEngine.levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MovingEngine
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            INIParser.DefaultValues = new Dictionary<string, Dictionary<string, ValueType>>()
        {
            {
                "General", new Dictionary<string, ValueType>()
                {
                    { "ForceDefault", false }
                }
            },
            {
                "Graphics", new Dictionary<string, ValueType>()
                {
                    { "CalculatedGlowEffectEnabled", true },
                    { "CalculatedGlowEffectRadius", 25 },
                    { "UseHardcodedGlowForPlayerAndEnemies", true }
                }
            },
            {
                "LevelGeneration", new Dictionary<string, ValueType>()
                {
                    { "EnemiesDependingOnStage", true },
                    { "MaxEnemies", 15 },
                    { "RandomizedStageSize", false },
                    { "FixedStageSizeMultipierX", 0 },
                    { "FixedStageSizeMultipierY", 0 },
                    { "MaxObjectsAndEnemiesPerStage", 15 }
                }
            }
        };
            InitializeComponent();
            Content = Globals.canvas;
            
            Globals.fontSizeMultiplier = SystemParameters.VirtualScreenWidth / 1936;
            if (Globals.fontSizeMultiplier > 1)
                Globals.fontSizeMultiplier = 1;
            FontFamily = (FontFamily)Resources["FixedSys"];
            Globals.window = this;
            Show();
            Globals.loading = new Canvas
            {
                Background = Brushes.Black,
                Width = Globals.canvas.ActualWidth,
                Height = Globals.canvas.ActualHeight
            };

            Label Loading = new Label
            {
                Content = "LOADING...",
                FontSize = 100 * Globals.fontSizeMultiplier,
                Foreground = Brushes.White
            };
            Globals.loading.Children.Add(Loading);
            Globals.canvas.Children.Add(Globals.loading);
            Canvas.SetBottom(Loading, 0);
            Canvas.SetRight(Loading, 0);
            Canvas.SetZIndex(Globals.loading, 1000);

            SpriteList.Load(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data\\textures"));

            Globals.player.HP = Globals.player.baseHP;

            Rectangle hpouter = new Rectangle { Fill = Brushes.Black, Height = 90, Width = Globals.canvas.ActualWidth - 20 };
            Rectangle hpborder = new Rectangle { Fill = Brushes.White, Height = 70, Width = Globals.canvas.ActualWidth - 40 };
            Rectangle hpinner = new Rectangle { Fill = Brushes.Red, Height = 50, Width = Globals.canvas.ActualWidth - 60 };

            Globals.hpbar = hpinner;
            Globals.hpbarlength = (int)hpinner.Width;

            Canvas.SetLeft(hpouter, 10);
            Canvas.SetLeft(hpborder, 20);
            Canvas.SetLeft(hpinner, 30);
            Canvas.SetTop(hpouter, 10);
            Canvas.SetTop(hpborder, 20);
            Canvas.SetTop(hpinner, 30);
            Canvas.SetZIndex(hpouter, 5);
            Canvas.SetZIndex(hpborder, 5);
            Canvas.SetZIndex(hpinner, 5);
            Globals.canvas.Children.Add(hpouter);
            Globals.canvas.Children.Add(hpborder);
            Globals.canvas.Children.Add(hpinner);
            Globals.HPbars.Add(hpouter);
            Globals.HPbars.Add(hpborder);
            
            Label hptext = new Label { Height = 50, Width = Globals.canvas.ActualWidth - 60, Foreground = Brushes.Black, Content = "0 / 0", HorizontalContentAlignment = HorizontalAlignment.Center, FontSize = 32 * Globals.fontSizeMultiplier };
            Canvas.SetLeft(hptext, 30);
            Canvas.SetTop(hptext, 30);

            Globals.hptext = hptext;
            Canvas.SetZIndex(hptext, 5);

            Globals.canvas.Children.Add(hptext);

            MouseMove += MouseSaver;
            MouseDown += (a,b) => Globals.MouseHandler.Add(b.ChangedButton);
            MouseUp += (object sender, MouseButtonEventArgs e) => Globals.MouseHandler.Remove(e.ChangedButton);
            Globals.canvas.Children.Add(Globals.player.visual);
            Canvas.SetZIndex(Globals.player.visual, 2);

            Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
            Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            Globals.canvas.SizeChanged += (object sender, SizeChangedEventArgs g) =>
            {
                Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
                Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            };

            if (File.Exists(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "settings.ini")))
            {
                Globals.settings = INIParser.Load(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "settings.ini"));
                if (Globals.settings.ContainsKey("General")) if ((bool)Globals.settings["General"]["ForceDefault"]) Globals.settings = new Dictionary<string, Dictionary<string, ValueType>>();
            } 
            foreach (KeyValuePair<string, Dictionary<string, ValueType>> key in INIParser.DefaultValues)
            {
                if (!Globals.settings.ContainsKey(key.Key)) Globals.settings.Add(key.Key, new Dictionary<string, ValueType>());
                foreach (KeyValuePair<string, ValueType> key1 in key.Value)
                {
                    if (!Globals.settings[key.Key].ContainsKey(key1.Key)) Globals.settings[key.Key].Add(key1.Key, key1.Value);
                }
            }
            File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "settings.ini"), INIParser.Encode(Globals.settings));
            if (!(bool)Globals.settings["Graphics"]["UseHardcodedGlowForPlayerAndEnemies"])
            {
                if ((bool)Globals.settings["Graphics"]["CalculatedGlowEffectEnabled"]) Globals.player.visual.Effect =
                    new DropShadowEffect
                    {
                        Color = Brushes.Black.Color,
                        Direction = 0,
                        ShadowDepth = 0,
                        Opacity = 1,
                        BlurRadius = (int)Globals.settings["Graphics"]["CalculatedGlowEffectRadius"]
                    };
            }
            else
            {
                Image Glow = new Image
                {
                    Source = SpriteList.List["player_g"],
                    Width = 200,
                    Height = 200
                };
                Canvas.SetLeft(Glow, -65);
                Canvas.SetTop(Glow, -65);
                Globals.player.visual.Children.Add(Glow);
            }
            Label tps = new Label { Content = 0, Foreground = Brushes.White, Background = Brushes.Black, FontSize = 30 };
            Canvas.SetLeft(tps, 0);
            Canvas.SetBottom(tps, 0);
            Globals.player.visual.Children.Add(tps);

            GameLoop loop = new GameLoop();

            Globals.gamelooptimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000 / Globals.DispatcherTime)};
            Globals.gamelooptimer.Tick += (sender, e) => loop.loop();
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, e) =>
            {
                tps.Content = Globals.tps;
                Globals.tps = 0;
            };
            timer.Start();

            Globals.player.currentStage = 1;
            GameLoop.rungame(0);
            MainMenu.Start();

            Collision.InitDebug();
        }

        private void MouseSaver(object sender, MouseEventArgs e)
        {
            Globals.mouse_position = e.GetPosition(this);
        }
    }
}
