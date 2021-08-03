using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLog
{
    public class TotalLog
    {
        public string Request { get; set; }

        public int StatusCode { get; set; }

        public DateTime EndResponse { get; set; }

        public long CompletedRequest { get; set; }

        public double ResponseTime { get; set; }

        public int SentBytes { get; set; }

        public int ReceivedBytes { get; set; }

        public TotalLog(string request,
            int statusCode,
            DateTime endResponse,
            long completedRequest,
            double responseTime,
            int sentBytes,
            int receivedBytes)
        {
            Request = request;
            StatusCode = statusCode;
            EndResponse = endResponse;
            CompletedRequest = completedRequest;
            ResponseTime = responseTime;
            SentBytes = sentBytes;
            ReceivedBytes = receivedBytes;
        }
    }
}
