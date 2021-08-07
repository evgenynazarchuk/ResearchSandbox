using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLog
{
    public class RawLogMessage
    {
        public string User { get; set; }

        public string ProtocolType { get; set; }

        public string Request { get; set; }

        public string RequestLabel { get; set; }

        public int StatusCode { get; set; }

        public long StartSendRequest { get; set; }

        public long StartWaitResponse { get; set; }

        public long StartResponse { get; set; }

        public long EndResponse { get; set; }

        public int SendBytes { get; set; }

        public int ReceiveBytes { get; set; }

        public RawLogMessage(
            string user,
            string protocolType,
            string request,
            string requestLabel,
            int statusCode,
            long startSendRequest,
            long startWaitResponse,
            long startResponse,
            long endResponse,
            int sendBytes,
            int receiveBytes)
        {
            User = user;
            ProtocolType = protocolType;
            Request = request;
            RequestLabel = requestLabel;
            StatusCode = statusCode;
            StartSendRequest = startSendRequest;
            StartWaitResponse = startWaitResponse;
            StartResponse = startResponse;
            EndResponse = endResponse;
            SendBytes = sendBytes;
            ReceiveBytes = receiveBytes;
        }
    }
}
