using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.CursorVisible = false;
            int height = Console.WindowHeight - 1;
            int width = Console.WindowWidth - 5;
            bool shouldExit = false;

            List<(int, int)> snake = new List<(int, int)> { (0, 0) };
            string player = "#";
            int playerLength = player.Length;
            bool playerCanMove = true;


            int foodX = 0;
            int foodY = 0;
            string food = "@";

            InitializeGame();
            while (!shouldExit)
            {
                if (TerminalResized())
                {
                    Console.Clear();
                    Console.Write("Console was resized. Program exiting.");
                    shouldExit = true;
                }
                else
                {
                    if (playerCanMove)
                    {
                        KeyBoardListener(1, false);
                    }
                    else
                    {
                        KeyBoardListener(otherKeysExit: false);
                    }
                    if (GotFood())
                    {
                        ExtendSnake();
                        ShowFood();
                    }
                }
            }


            void KeyBoardListener(int speed = 1, bool otherKeysExit = false)
            {
                var (lastX, lastY) = snake[0];
                int newX = lastX, newY = lastY;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        newY--;
                        break;
                    case ConsoleKey.DownArrow:
                        newY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        newX -= speed;
                        break;
                    case ConsoleKey.RightArrow:
                        newX += speed;
                        break;
                    case ConsoleKey.Escape:
                        shouldExit = true;
                        break;
                    default:
                      
                        shouldExit = otherKeysExit;
                        break;
                }


                foreach (var (x, y) in snake)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }

                
                snake.Insert(0, (newX, newY));
                if (snake.Count > playerLength)
                {
                    snake.RemoveAt(snake.Count - 1);
                }

                
                snake[0] = (
                    snake[0].Item1 < 0 ? 0 : (snake[0].Item1 >= width ? width : snake[0].Item1),
                    snake[0].Item2 < 0 ? 0 : (snake[0].Item2 >= height ? height : snake[0].Item2)
                );

               
                foreach (var (x, y) in snake)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(player[0]);
                }
            }

        
            void ShowFood()
            {
               
                do
                {
                    foodX = random.Next(0, width - player.Length);
                    foodY = random.Next(0, height - 1);
                } while (snake.Contains((foodX, foodY)));

           
                Console.SetCursorPosition(0, height);
                Console.Write($"Food at: ({foodX}, {foodY})   "); 


                Console.SetCursorPosition(foodX, foodY);
                Console.Write(food);
            }

         
            bool GotFood()
            {
                return snake[0].Item1 == foodX && snake[0].Item2 == foodY;
            }

          
            void ExtendSnake()
            {
                playerLength += player.Length; 
            }

     
            void InitializeGame()
            {
                Console.Clear();
                ShowFood();
                Console.SetCursorPosition(0, 0);
                Console.Write(player);
            }

            bool TerminalResized()
            {
                return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
            }



        }
         }
    }

