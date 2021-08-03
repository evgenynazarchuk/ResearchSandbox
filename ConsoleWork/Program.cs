using System;
using System.Runtime.InteropServices;

namespace ConsoleWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            //for (int i = 1; i <= Console.WindowHeight; i++)
            //{
            //    Console.WriteLine($"{i}");
            //}

            while (true)
            {
                PrintConsoleInfo();

                int n = 1;
                for (int i = 1; i <= Console.BufferWidth; i++)
                {
                    Console.Write($"{n++}");
                    if (n == 9)
                        n = 1;
                }
                Console.WriteLine();
                Console.ReadKey();
            }


            // Console.ResetColor();
        }

        static void PrintConsoleInfo()
        {
            Console.WriteLine($"BufferWidth:\t\t\t{Console.BufferWidth}");
            Console.WriteLine($"BufferHeight\t\t\t{Console.BufferHeight}");

            Console.WriteLine($"WindowWidth\t\t\t{Console.WindowWidth}");
            Console.WriteLine($"WindowHeight\t\t\t{Console.WindowHeight}");
            
            Console.WriteLine($"WindowLeft\t\t\t{Console.WindowLeft}");
            Console.WriteLine($"WindowTop\t\t\t{Console.WindowTop}");
        }
    }

    public class ConsoleWindow
    {
        public int Width { get => Console.WindowWidth; }

        public int Height { get => Console.WindowHeight; }

        public int[,] Window;

        public ConsoleWindow CreateWindow()
        {
            Window = new int[Width, Height];

            return this;
        }
    }
}
