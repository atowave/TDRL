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
            MouseMove += MouseSaver;
            Globals.canvas.Children.Add(Globals.player.visual);
            Canvas.SetZIndex(Globals.player.visual, 2);
            Show();

            Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
            Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            Globals.canvas.SizeChanged += (object sender, SizeChangedEventArgs g) =>
            {
                Canvas.SetTop(Globals.player.visual, (Globals.canvas.ActualHeight - Globals.player.visual.ActualHeight) / 2);
                Canvas.SetLeft(Globals.player.visual, (Globals.canvas.ActualWidth - Globals.player.visual.ActualWidth) / 2);
            };
            Globals.canvas.Children.Add(Globals.Debug);

            DispatcherTimer gamelooptimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1)};
            gamelooptimer.Tick += (object sender, EventArgs e) => new GameLoop();
            gamelooptimer.Start();
            LoadLevel();
        }

        private void MouseSaver(object sender, MouseEventArgs e)
        {
            Globals.mouse_position = e.GetPosition(this);
        }

        private void LoadLevel()
        {
            baselevel level1 = new level1();
            level1.start();
            level1.Bulid_Level();
        }
    }
}
