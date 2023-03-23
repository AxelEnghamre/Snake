using System;
using System.Drawing;

namespace Snake
{
	public class Game
	{
        ScheduleTimer? _timer;
        public bool Paused { get; private set; }
        public bool GameOver { get; private set; }

        private int _width;
        private int _height;
        private int[,] _field;
        private Point _food;
        private Snake _snake;


        public Game(int Width, int Height)
        {
            _width = Width;
            _height = Height;

            // Set the field
            _field = new int[_height, _width];

            // Create the snake snake
            _snake = new Snake(_width, _height,_width/2,_height/2);

            // Create food
            _setFood();
        }


        // Food
        private void _printFood()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(_food.X, _food.Y);
            Console.Write(" ");
        }

        private void _setFood()
        {
            // Create random food
            Random _random = new();
            var x = _random.Next(1, _width);
            var y = _random.Next(1, _height - 1);

            _food = new Point(x, y);
        }

        // Field
        private void _printField()
        {
            Console.BackgroundColor = ConsoleColor.Red;

            for (var y = 0; y < _field.GetLength(0); y++)
            {
                Console.Write(" ");

                for (var x = 0; x < _field.GetLength(1); x++)
                {
                    if (y == 0 || y == _field.GetLength(0) - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(" ");
            }
        }


        // Game
        public void Start()
        {
            Console.WriteLine("Start");
            Thread.Sleep(1000);
            ScheduleNextTick();
        }

        public void Pause()
        {
            Console.Clear();
            Console.WriteLine("Pause");
            Paused = true;
            _timer!.Pause();
        }

        public void Resume()
        {
            Console.WriteLine("Resume");
            Paused = false;
            Thread.Sleep(1000);
            _timer!.Resume();
        }

        public void Stop()
        {
            GameOver = true;
            Console.Clear();
            Console.WriteLine("Game Over!");
            PrintScore();
            Thread.Sleep(1000);
        }

        public void Input(ConsoleKey key)
        {
            _snake.SetSnakeDirection(key);
        }

        public void PrintScore()
        {
            Console.WriteLine("Score: ");
            Console.Write(_snake.GetScore());
        }

        void Tick()
        {
            if (!GameOver)
            {
                ScheduleNextTick();
                Console.Clear();

                // Print the filed
                _printField();

                // Print the food
                _printFood();

                // Print the snake
                _snake.PrintSnake();

                // Move the snake
                _snake.MoveSnake();

                // Check if snake has hit food
                if(_snake.CheckFood(_food))
                {
                    // Create new food
                    _setFood();
                }

                // Check if snake is not alive
                if(!_snake.CheckSnake())
                {
                    Stop();
                }

                // Print the current score
                Console.SetCursorPosition(0, _height);
                PrintScore();
            }
        }

        void ScheduleNextTick()
        {
            // the game will automatically update itself every half a second, adjust as needed
            _timer = new ScheduleTimer(100, Tick);
        }
    }
}

