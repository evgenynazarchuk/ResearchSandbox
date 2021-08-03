using System;
using System.IO;

namespace AsyncReadStream
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10000; i++)
            {
                File.AppendAllText("source.txt", $"number: {i}\n");
            }
        }
    }
}
