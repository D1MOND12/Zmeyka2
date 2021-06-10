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
        int _elementSize = 20;
        private int _numberOfRows;
        private int _numberOfColumns;

        DispatcherTimer _gameTimer;
        List<Snake> _snake;
        private Direction _currentDirection;

        public MainWindow()
        {
            InitializeComponent();
            InistalizeTimer();
            DrawGame();
            InitializeSnake();
            DrawSnake();
        }

        private void DrawSnake()
        {
            foreach (var snake in _snake)
            {
                if (!Game.Children.Contains(snake.UIElement))
                    Game.Children.Add(snake.UIElement);

                Canvas.SetLeft(snake.UIElement, snake.X);
                Canvas.SetTop(snake.UIElement, snake.Y);
            }
        }

        private void InitializeSnake()
        {
            _snake = new List<Snake>();
            _snake.Add(new Snake(_elementSize)
            {
                X = (_numberOfColumns / 2) * _elementSize,
                Y = (_numberOfColumns / 2) * _elementSize
            });
            _currentDirection = Direction.Left;
        }

        private void DrawGame()
        {
            _numberOfColumns =(int) this.Width / _elementSize;
            _numberOfRows = (int) this.Height / _elementSize;

            for (int i = 0; i < _numberOfRows; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = 0;
                line.Y1 = i * _elementSize;
                line.X2 = Width;
                line.Y2 = i * _elementSize;
                Game.Children.Add(line); 
            }
            for (int i = 0; i < _numberOfColumns; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = i * _elementSize;
                line.Y1 = 0;
                line.X2 = i * _elementSize;
                line.Y2 = Height;
                Game.Children.Add(line);
            }
        }

        public void InistalizeTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromSeconds(0.5);
            _gameTimer.Tick += MainGameLoop;
            _gameTimer.Start();
        }

        private void MainGameLoop(object sender, EventArgs e)
        {
            MoveSnake();
            DrawSnake();
        }

        private void MoveSnake()
        {
            foreach (var snake in _snake)
            {
                switch (_currentDirection)
                {
                    case Direction.Right:
                        snake.X += _elementSize;
                        break;
                    case Direction.Left:
                        snake.X -= _elementSize;
                        break;
                    case Direction.Up:
                        snake.Y += _elementSize;
                        break;
                    case Direction.Down:
                        snake.Y -= _elementSize;
                        break;

                }
                snake.X += _elementSize;
            }
        }

        private void KeyWasReleased(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    _currentDirection = Direction.Up;
                    break;
                case Key.A:
                    _currentDirection = Direction.Left;
                    break;
                case Key.S:
                    _currentDirection = Direction.Down;
                    break;
                case Key.D:
                    _currentDirection = Direction.Right;
                    break;
            }
        }
    }


    enum Direction{ 
        Right,
        Left,
        Up,
        Down
    }
}
