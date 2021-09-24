using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Reflection;
using System.Threading;

namespace Logger
{
    public class UniversalLogger : IUniversalLogger
    {
        public readonly CancellationToken Token;

        public readonly CancellationTokenSource TokenSource;

        protected readonly ConcurrentQueue<(string logName, string logMessage, Type logType)> logQueue;

        protected readonly Dictionary<string, StreamWriter> writers;

        public ConcurrentQueue<(string logName, string logMessage, Type logType)> LogQueue { get => this.logQueue; }

        public Dictionary<string, StreamWriter> Writers { get => this.writers; }

        public UniversalLogger()
        {
            this.logQueue = new();
            this.writers = new();
        }

        // append log
        public virtual void AppendLogMessage(string logName, string logMessage, Type logMessageType)
        {
            this.logQueue.Enqueue((logName, logMessage, logMessageType));
        }

        // save logs
        public virtual void StartProcessing()
        {
            while (true)
            {
                if (this.logQueue.IsEmpty && this.Token.IsCancellationRequested)
                {
                    break;
                }
                this.logQueue.TryDequeue(out (string logName, string logMessage, Type logMessageType) log);

                var logWriter = this.writers.GetValueOrDefault(log.logName);
                if (logWriter is null)
                {
                    logWriter = new StreamWriter(log.logName, false, Encoding.UTF8, 65535);
                    this.writers.Add(log.logName, logWriter);
                }

                if (logWriter is not null)
                {
                    var jsonLogMessage = this.Convert(log.logMessage, log.logMessageType);
                    logWriter.WriteLine(jsonLogMessage);
                }
                else
                {
                    throw new ApplicationException("Log Writer is not set");
                }
            }

            this.Finish();
        }

        public virtual void StopProcessing()
        {
            this.TokenSource.Cancel();
        }

        public virtual void Finish()
        {
            foreach ((var logName, var fileWriter) in this.writers)
            {
                fileWriter.Flush();
                fileWriter.Close();

                this.PostProcessing(logName);
            }

            this.PostProcessing();
        }

        // after finish
        public virtual void PostProcessing(string logName)
        { 
        }

        public virtual void PostProcessing()
        {
        }

        // from string to string
        public virtual string Convert(string logMessage, Type logMessageType)
        {
            var obj = this.GetObjectFromRawCsvLogMessage(logMessage, logMessageType);
            var jsonString = this.GetJsonStringFromObject(obj, logMessageType);
            return jsonString;
        }

        // from csv text to object
        public virtual object GetObjectFromRawCsvLogMessage(string logMessage, Type logMessageType)
        {
            var logMessageObject = Activator.CreateInstance(logMessageType);
            var logProperties = logMessageObject.GetType().GetProperties();

            var splittedLog = logMessage.Split(",");

            int currentItemPosition = 0;
            foreach (var item in splittedLog)
            {
                logProperties[currentItemPosition].SetValue(logMessageObject, item);
                currentItemPosition++;
            }

            return logMessageObject;
        }

        // from object to json string
        public virtual string GetJsonStringFromObject(object logMessageObject, Type logMessageType)
        {
            var jsonString = JsonSerializer.Serialize(logMessageObject, logMessageType);
            return jsonString;
        }
    }
}
