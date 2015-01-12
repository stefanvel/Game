using System;
using System.Threading;


namespace game
{
    class Program
    {

        static int[,] aÎbstacle = new int[8, 8] {
            { 0, 2, 3, 4, 2, 3, 4, 2 },
            { 1, 0, 1, 1, 2, 3, 4, 2 },
            { 2, 1, 0, 2, 2, 3, 4, 2 },
            { 3, 3, 3, 0, 2, 3, 4, 2 },
            { 3, 3, 3, 0, 2, 3, 4, 2 },
            { 3, 3, 3, 0, 2, 3, 4, 2 },
            { 3, 3, 3, 0, 2, 3, 4, 2 },
            { 4, 4, 1, 1, 2, 3, 4, 2 }
        };

        static int padSize = 10;
        static int padRow = 0;
        static int padCol = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = true;
        static int ballRow = 15;
        static int ballCol = 15;
        static double ballSpeedUp = 0.1;
        static double ballSpeedRight = 0.1;

        static void printObstacle()
        {
            int rowLength = aÎbstacle.GetLength(0);
            int colLength = aÎbstacle.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    //Console.WriteLine(string.Format("{0} ", aÎbstacle[i, j]));
                    if (aÎbstacle[i, j] > 0)
                    {
                        printEl(j, i, '@');
                    }
                }
            }
        }

        static void isDestroyObstacle()
        {
            int rowLength = aÎbstacle.GetLength(0);
            int colLength = aÎbstacle.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    //Console.WriteLine(string.Format("{0} ", aÎbstacle[i, j]));
                    if (aÎbstacle[i, j] > 0)
                    {
                        //in target
                        if(ballCol == j && ballRow == i)
                        {
                            //remove Obstacle
                            aÎbstacle[i, j] = 0;
                            //change ball directions
                            ballDirectionRight = !ballDirectionRight;
                            ballDirectionUp = !ballDirectionUp;
                        }
                    }
                }
            }
        }

        static void printPad()
        {
            for(int i = 0; i < padSize; i++)
            {
                printEl(padCol + i, padRow, '=');
            }
        }

        static bool printBall()
        {

            if (ballRow == Console.BufferHeight - 2)
            {
                if(ballCol >= padCol && ballCol <= (padCol + padSize))
                {
                    ballDirectionRight = !ballDirectionRight;
                    ballDirectionUp = !ballDirectionUp;
                }                
            }

            if (ballDirectionRight)
            {
                if (ballCol < Console.BufferWidth-1)
                {
                    ballCol++;
                }
                else
                {
                    ballDirectionRight = false;
                }
            }
            else
            {
                if(ballCol > 0)
                {
                    ballCol--;
                }
                else
                {
                    ballDirectionRight = true;
                }
            }

            if(ballDirectionUp)
            {
                if(ballRow > 0)
                {
                    ballRow--;
                }
                else
                {
                    ballDirectionUp = false;
                }
            }
            else
            {
                if (ballRow < Console.BufferHeight - 1)
                {
                    ballRow++;
                }
                else
                {
                    return true;
                    ballDirectionUp = true;
                }
            }

            printEl(ballCol, ballRow, '*');
            return false;
        }

        static void printEl(int col, int row, char c)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(c);
        }

        static void initSreen()
        {
            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 30;
            Console.Title = "Game";
        }

        static void initPadPosition()
        {
            padRow = Console.BufferHeight - 1;
            padCol = Console.BufferWidth / 2;
        }

        static void initBallPosition()
        {
            ballRow = Console.BufferHeight / 2;
            ballCol = Console.BufferWidth / 2;
        }

        static void movePad(string direction)
        {
            if(direction == "left")
            {
                if(padCol > 0)
                {
                   padCol--;
                }
            }

            if(direction == "right")
            {
                if(padCol < Console.BufferWidth-padSize)
                {
                    padCol++;
                }
            }
        }

        static void Main()
        {
            initSreen();
            initPadPosition();
            initBallPosition();
            printPad();
            printObstacle();
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        movePad("left");
                    }

                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        movePad("right");
                    }
                }

                Console.Clear();
                printPad();
                printObstacle();
                isDestroyObstacle();
                if(printBall())
                {
                    Console.Clear();
                    Console.SetCursorPosition((Console.BufferWidth / 2)-4, Console.BufferHeight / 2);
                    Console.WriteLine("Game Over");
                    break;
                }
                
                Thread.Sleep(60);
            } 
        }
    }
}
