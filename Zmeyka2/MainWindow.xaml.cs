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
using Zmeyka2.Элементы;


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
        List<Snake> _apples;
        List<Apple> _gameEnetities;
        private Random _randoTron;

        Direction _currentDirection;
        private double _gameWidth;
        private double _gameHeight;
        private long _elaspedTricks;
        public MainWindow()
        {
            InitializeComponent();
            _randoTron = new Random(DateTime.Now.Millisecond / DateTime.Now.Second);
            _apples = new List<Apple>();
            InistalizeTimer();
            DrawGame();
            InitializeSnake();
            DrawSnake();
        }


        private void InitializeSnake()
        {
            _apples = new List<Snake>();
            _apples.Add(new Snake(_elementSize)
            {
                X = _numberOfColumns / 2 * _elementSize,
                Y = _numberOfColumns / 2 * _elementSize,
                IsHead = true
            });
            _currentDirection = Direction.Left;
        }

        private void DrawGame()
        {
            _gameWidth = Width;
            _gameHeight = Height;
            _numberOfColumns = (int) _gameWidth / _elementSize;
            _numberOfRows = (int) _gameHeight / _elementSize;

            for (int i = 0; i < _numberOfRows; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = 0;
                line.Y1 = i * _elementSize;
                line.X2 = _gameWidth;
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
                line.Y2 = _gameHeight;
                Game.Children.Add(line);
            }
        }

        public void InistalizeTimer()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromSeconds(0.2);
            _gameTimer.Tick += MainGameLoop;
            _gameTimer.Start();
        }

        private void MainGameLoop(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            DrawSnake();
            CreateApple();
            DrawApples();
        }

        private void DrawSnake()
        {
            foreach (var snake in _apples)
            {
                if (!Game.Children.Contains(snake.UIElement))
                    Game.Children.Add(snake.UIElement);

                Canvas.SetLeft(snake.UIElement, snake.X);
                Canvas.SetTop(snake.UIElement, snake.Y);
            }
        }
        private void DrawApples()
        {
            foreach (var apple in _apples)
            {
                if (!Game.Children.Contains(apple.UIElement))
                    Game.Children.Add(apple.UIElement);

                Canvas.SetLeft(apple.UIElement, apple.X);
                Canvas.SetTop(apple.UIElement, apple.Y);
            }
        }

        private void CreateApple()
        {
            if (_elaspedTricks / 13 == 0)
            {
                _apples.Add(new Apple(_elementSize) {
                    X = _randoTron.Next(0, _numberOfColumns) * _elementSize,
                    Y = _randoTron.Next(0, _numberOfRows) * _elementSize
                });
            }
        }

        private void CheckCollision()
        {
            CheckCollisionWithWorldBounds();
            CheckCollisionWithSelf();
            CheckCollisionWithWorldItems();
        }

        private void CheckCollisionWithWorldItems()
        {
            Snake snakeHead = GetSnakeHead();

            if (snakeHead.X > _gameWidth - _elementSize || snakeHead.X < 0 || snakeHead.Y < 0 || snakeHead.Y > _gameHeight - _elementSize)
            {
                MessageBox.Show("Игра окончена!");
            }
        }

        private void CheckCollisionWithSelf()
        {
            Snake snakeHead = GetSnakeHead();
            if (snakeHead != null)
            {
                foreach (var snake in _apples)
                {
                    if (!snake.IsHead)
                    {
                        if (snake.X == snakeHead.X && snake.Y == snakeHead.Y)
                        {
                            MessageBox.Show("Игра окончена!");
                            break;
                        }
                    }
                }
            }
        }

        private Snake GetSnakeHead()
        {
            Snake snakeHead = null;
            foreach (var snake in _apples)
            {
                if (snake.IsHead)
                {
                    snakeHead = snake;
                    break;
                }
            }
            return snakeHead;
        }
        private void CheckCollisionWithWorldBounds()
        {

        }

        private void MoveSnake()
        {
            foreach (var snake in _apples)
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
                        snake.Y -= _elementSize;
                        break;
                    case Direction.Down:
                        snake.Y += _elementSize;
                        break;
                    default:
                        break;

                }
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
