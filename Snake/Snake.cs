using System;
using System.Drawing;

namespace Snake
{
	public class Snake
	{
        private List<Point> _snakeBody = new List<Point>();
        private int _snakeDirection = 0;
        private int _widthLimit, _heightLimit;

        public Snake(int width, int height, int startX = 20, int startY = 10)
		{
            _widthLimit = width;
            _heightLimit = height;


            // Set the head of the snake
            _snakeBody.Add(new Point(startX, startY));

        }

        public int GetScore()
        {
            return _snakeBody.Count;
        }


        public void PrintSnake()
        {
            // Print every snake position
            Console.BackgroundColor = ConsoleColor.Green;
            foreach (var position in _snakeBody)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
            }
        }


        public void MoveSnake()
        {
            // Make every snake position move forward one position
            for (int i = _snakeBody.Count - 2; i >= 0; i--)
            {
                _snakeBody[i + 1] = _snakeBody[i];
            }


            // Move the head of the snake
            var x = _snakeBody[0].X;
            var y = _snakeBody[0].Y;

            switch (_snakeDirection)
            {
                case 0:
                    x++;
                    if (x > _widthLimit)
                    {
                        x = 1;
                    }
                    break;
                case 1:
                    x--;
                    if (x <= 0)
                    {
                        x = _widthLimit;
                    }
                    break;
                case 2:
                    y++;
                    if (y >= _heightLimit - 1)
                    {
                        y = 1;
                    }
                    break;
                case 3:
                    y--;
                    if (y <= 0)
                    {
                        y = _heightLimit - 2;
                    }
                    break;
            }

            // Set the head of the snake to the new head position
            _snakeBody[0] = new Point(x, y);
        }

        public bool CheckSnake()
        {
            // Check if a snake position element hits another snake position element    
            var position1Index = 0;
            var position2Index = 0;
            foreach (var position1 in _snakeBody)
            {
                foreach (var position2 in _snakeBody)
                {
                    // if the position is the position
                    if (position1Index == position2Index)
                    {
                        continue;
                    }

                    // if the position is the same as another position
                    if (position1 == position2)
                    {
                        return false;
                    }


                    position2Index++;
                }
                position1Index++;
            }

            return true;
        }

        public bool CheckFood(Point food)
        {
            // Check if head of the snake hits the food
            if (_snakeBody[0] == food)
            {
                _snakeBody.Add(new Point());
                return true;
            }

            return false;
        }


        public void SetSnakeDirection(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.RightArrow:
                    _snakeDirection = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    _snakeDirection = 1;
                    break;
                case ConsoleKey.DownArrow:
                    _snakeDirection = 2;
                    break;
                case ConsoleKey.UpArrow:
                    _snakeDirection = 3;
                    break;
            }
        }
    }
}

