using System;
using System.Collections.Generic;
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

namespace Zmeyka2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _elementSize = 10;
        DispatcherTimer _gameTimer;
        public MainWindow()
        {
            InitializeComponent();
            InstalizeTimer();
            DrawGame();
        }

        private void DrawGame()
        {
            var numberOfColumns = this.Width / _elementSize;
            var numberOfRows = this.Height / _elementSize;

            for (int i = 0; i < numberOfRows; i++)
            {
                Lint line = new Line();
                line.Stroke = Brushes.Black;

            }
        }

        public void InstalizeTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromSeconds(0.5);
            _gameTimer.Tick += MainGameLoop;
            _gameTimer.Start();
        }

        private void MainGameLoop(object sender, EventArgs e)
        {

        }
    }
}
