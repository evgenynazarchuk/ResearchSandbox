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

        void AppendLogMessage(string logName, string logMessage, Type logMessageType);

        string Convert(string logMessage, Type logMessageType) => logMessage;

        void StartProcessing();

        void StopProcessing();

        void Finish();

        void PostProcessing(string logName);

        void PostProcessing();
    }
}
