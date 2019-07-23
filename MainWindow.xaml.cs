using MovingEngine.classes;
using MovingEngine.levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
            InitializeComponent();
            Globals.canvas = new Canvas { Background = Brushes.Black};
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

            MouseMove += MouseSaver;
            MouseDown += GameLoop.Mouse;
            MouseUp += (object sender, MouseButtonEventArgs e) => Globals.MouseHandler.Pressed = MouseButtonState.Released;
            Globals.canvas.Children.Add(Globals.player.visual);
            Canvas.SetZIndex(Globals.player.visual, 2);

            Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
            Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            Globals.canvas.SizeChanged += (object sender, SizeChangedEventArgs g) =>
            {
                Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
                Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            };
            GameLoop loop = new GameLoop();
            Globals.gamelooptimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000 / 60)};
            Globals.gamelooptimer.Tick += (object sender, EventArgs e) => loop.loop();

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
