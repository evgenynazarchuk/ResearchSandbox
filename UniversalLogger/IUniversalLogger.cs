using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface IUniversalLogger
    {
        ConcurrentQueue<(string logName, string logMessage, Type logType)> LogQueue { get; }

        Dictionary<string, StreamWriter> Writers { get; }
    }
}
