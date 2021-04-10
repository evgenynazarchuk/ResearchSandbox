using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;


namespace NonBlockingAccess
{
    public class ConcurrentConsoleOutput
    {
        private readonly ConcurrentQueue<string> _queue;
        private readonly TimeSpan _totalPerformancePlansDuration;

        public ConcurrentConsoleOutput(TimeSpan totalPerformancePlansDuration)
        {
            this._queue = new();
            this._totalPerformancePlansDuration = totalPerformancePlansDuration;
        }

        public void SendMessage(string message)
        {
            this._queue.Enqueue(message);
        }

        public void StartMonitor()
        {
            var endDateTime = DateTime.Now.Add(this._totalPerformancePlansDuration);
            while (DateTime.Now.CompareTo(endDateTime) < 0)
            {
                if (this._queue.IsEmpty)
                    continue;

                string message;
                if (this._queue.TryDequeue(out message))
                {
                    Console.Write(message);
                }
            }
        }
    }
}
