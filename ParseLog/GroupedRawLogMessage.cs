using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLog
{
    public class GroupedRawLogMessage
    {
        public string User { get; set; }

        public string Request { get; set; }

        public int StatusCode { get; set; }

        public DateTime EndResponse { get; set; }

        public long CompletedRequest { get; set; }

        public double ResponseTime { get; set; }

        public double SentTime { get; set; }

        public double WaitTime { get; set; }

        public double ReceivedTime { get; set; }

        public GroupedRawLogMessage(
            string user,
            string request,
            int statusCode,
            DateTime endResponse,
            long completedRequest,
            double responseTime,
            double sentTime,
            double waitTime,
            double receiveTime)
        {
            User = user;
            Request = request;
            StatusCode = statusCode;
            EndResponse = endResponse;
            CompletedRequest = completedRequest;
            ResponseTime = responseTime;
            SentTime = sentTime;
            WaitTime = waitTime;
            ReceivedTime = receiveTime;
        }
    }
}
