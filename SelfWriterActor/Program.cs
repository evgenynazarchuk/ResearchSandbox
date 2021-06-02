using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SelfWriterActor
{
    class Program
    {
        static void Main()
        {
            Stopwatch watch = new();
            LogWrite logWriter = new();

            watch.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                logWriter.Write("Hello world! Hello world! Hello world! Hello world! ");
            }
            watch.Stop();

            Console.WriteLine($"Call time: {watch.ElapsedMilliseconds} ms");

            string userInput;
            while (true)
            {
                Console.Write("Save(yes/no)? ");
                userInput = Console.ReadLine();
                if (userInput == "yes")
                {
                    logWriter.Cancel = true;
                    break;

                }
                    
            }

            logWriter.DisposeAsync().GetAwaiter().GetResult();
        }
    }

    // do not work
    class LogWrite : IAsyncDisposable
    {
        private readonly ConcurrentQueue<string> Queue = new();
        private readonly StreamWriter fileStream = new("result.txt", false, Encoding.UTF8, 65535);
        private Task LogFileWriteTask;
        public bool Cancel = false;

        public LogWrite()
        {
            LogFileWriteTask = StartAsync();
        }

        public void Write(string message)
        {
            Queue.Enqueue(message);
        }

        private async Task StartAsync()
        {
            while (!Queue.IsEmpty && !Cancel)
            {
                Queue.TryDequeue(out string message);
                fileStream.WriteLine(message);
                fileStream.Flush();
            }

            await Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(1000);
            LogFileWriteTask.Dispose();
            fileStream.Flush();
            fileStream.Close();
        }
    }
}
