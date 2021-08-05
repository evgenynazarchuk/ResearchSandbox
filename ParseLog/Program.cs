using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ParseLog
{
    class Program
    {
        static void Main(string[] args)
        {
            var readfile = "result 1.log";
            var outputfile = "report 1.html";

            StreamReader reader = new(readfile, Encoding.UTF8, false, 65535);
            List<RawLogMessage> rawLogMessages = new();

            string? rawLogAsString;
            RawLogMessage? rawLogMessage;
            while ((rawLogAsString = reader.ReadLine()) != null)
            {
                rawLogMessage = JsonSerializer.Deserialize<RawLogMessage>(rawLogAsString);
                if (rawLogMessage is null) break;
                rawLogMessages.Add(rawLogMessage);
            }
            reader.Close();

            var groupByRequestStatusCodeEndResponse = rawLogMessages
                .GroupBy(x => new { x.User, x.Request, x.StatusCode, EndResponse = x.EndResponse.Trim() })
                .Select(x => new
                {
                    x.Key,
                    CompletedRequest = x.LongCount(),
                    SentBytes = x.Sum(y => y.SendBytes),
                    ReceivedBytes = x.Sum(y => y.ReceiveBytes),
                    ResponseTime = x.Average(y => y.EndResponse.Subtract(y.StartSendRequest).Ticks),
                    SentTime = x.Average(y => y.StartWaitResponse.Ticks - y.StartSendRequest.Ticks),
                    WaitTime = x.Average(y => y.StartResponse.Subtract(y.StartWaitResponse).Ticks),
                    ReceivedTime = x.Average(y => y.EndResponse.Ticks - y.StartResponse.Ticks)
                });

            StreamWriter reportWriter = new(outputfile, false, Encoding.UTF8, 65355);
            StringBuilder groupedStringLog = new();
            
            //
            foreach (var item in groupByRequestStatusCodeEndResponse)
            {
                GroupedRawLogMessage totalLog = new(
                    item.Key.User,
                    item.Key.Request,
                    item.Key.StatusCode,
                    item.Key.EndResponse,
                    item.CompletedRequest,
                    item.ResponseTime,
                    item.SentTime,
                    item.WaitTime,
                    item.ReceivedTime);
            
                groupedStringLog.Append(JsonSerializer.Serialize(totalLog) + ",\n");
            }

            //
            StringBuilder sentStringLog = new();
            StringBuilder receivedStringLog = new();

            var groupByEndResponse = rawLogMessages
                .GroupBy(x => new { EndResponse = x.EndResponse.Trim() })
                .Select(x => new
                {
                    EndResponse = x.Key.EndResponse,
                    SentBytes = x.Sum(y => y.SendBytes),
                    ReceivedBytes = x.Sum(y => y.ReceiveBytes)
                });

            foreach (var item in groupByEndResponse)
            {
                sentStringLog.Append(JsonSerializer.Serialize(new BytesCount(item.EndResponse, item.SentBytes)) + ",\n");
                receivedStringLog.Append(JsonSerializer.Serialize(new BytesCount(item.EndResponse, item.ReceivedBytes)) + ",\n");
            }

            //
            string sourceData = @$"

<script>
const data = [{groupedStringLog}]
const sentBytesLog = [{sentStringLog}]
const receivedBytesLog = [{receivedStringLog}]
</script>

";

            //
            string sentBytesDataset = @"
<script>

let sentBytesData = []
for (item of sentBytesLog)
{
    sentBytesData.push({ x: item.EndResponse, y: item.Count })
}


let sentBytesline = {}
sentBytesline.label = 'Sent Bytes'
sentBytesline.borderColor = 'rgb(54, 162, 235, 0.7)'
sentBytesline.backgroundColor = 'transparent'
sentBytesline.data = sentBytesData
sentBytesline.pointRadius = 1
sentBytesline.borderWidth = 1
let sentBytesDataset = []
sentBytesDataset.push(sentBytesline)

</script>
";

            //
            string receivedBytesDataset = @"
<script>

let receivedBytesData = []
for (item of receivedBytesLog)
{
    receivedBytesData.push({ x: item.EndResponse, y: item.Count })
}

let receivedBytesDataset = []
let receivedBytesline = {}
receivedBytesline.label = 'Received Bytes'
receivedBytesline.borderColor = 'rgb(54, 162, 235, 0.7)'
receivedBytesline.backgroundColor = 'transparent'
receivedBytesline.data = receivedBytesData
receivedBytesline.pointRadius = 1
receivedBytesline.borderWidth = 1
receivedBytesDataset.push(receivedBytesline)

</script>
";

            //
            string sentBytesChart = @"
<script>
let sentBytesCtx = document.getElementById('Sent Bytes').getContext('2d');
new Chart(sentBytesCtx, {
  type: 'line',
  data: { datasets: sentBytesDataset },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Sent Bytes'
      },
    },
  }
});
</script>
";

            //
            string receivedBytesChart = @"
<script>
let receivedBytesCtx = document.getElementById('Received Bytes').getContext('2d');
new Chart(receivedBytesCtx, {
  type: 'line',
  data: { datasets: receivedBytesDataset },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Received Bytes'
      },
    },
  }
});
</script>
";

            //
            string completedRequest_datasets = @"
<script>
let completedRequestsLabels = { };
for (let item of data)
{
    if (completedRequestsLabels[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        completedRequestsLabels[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
    completedRequestsLabels[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.CompletedRequest })
}

let completedRequestsLabelsDatasets = []
for(let key in completedRequestsLabels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = completedRequestsLabels[key]
    line.pointRadius = 1
    line.borderWidth = 1
    completedRequestsLabelsDatasets.push(line)
}

</script>
";

            //
            string responseTime_datasets = @"
<script>

let response_labels = { };

for (let item of data)
{
    if (response_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        response_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
    response_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.ResponseTime / 10000 })
}

let response_datasets = []
for(let key in response_labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = response_labels[key]
    line.borderWidth = 1
    line.pointRadius = 1
    response_datasets.push(line)
}

</script>
";

            //
            string sentTime_datasets = @"
<script>

let sentTime_labels = { };

for (let item of data)
{
    if (sentTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        sentTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
    sentTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.SentTime / 10000 })
}

let sentTime_datasets = []
for(let key in sentTime_labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = sentTime_labels[key]
    line.borderWidth = 1
    line.pointRadius = 1
    sentTime_datasets.push(line)
}

</script>
";
            //
            string waitTime_datasets = @"
<script>

let waitTime_labels = { };

for (let item of data)
{
    if (waitTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        waitTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
    waitTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.WaitTime / 10000 })
}

let waitTime_datasets = []
for(let key in waitTime_labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = waitTime_labels[key]
    line.borderWidth = 1
    line.pointRadius = 1
    waitTime_datasets.push(line)
}

</script>
";

            //
            string receivedTime_datasets = @"
<script>

let receivedTime_labels = { };

for (let item of data)
{
    if (receivedTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        receivedTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
    receivedTime_labels[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.ReceivedTime / 10000 })
}

let receivedTime_datasets = []
for(let key in receivedTime_labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = receivedTime_labels[key]
    line.borderWidth = 1
    line.pointRadius = 1
    receivedTime_datasets.push(line)
}

</script>
";

            
            //
            string drawGraphs = @"
<script>
let responseTimeCtx = document.getElementById('ResponseTime').getContext('2d');
new Chart(responseTimeCtx, {
  type: 'line',
  data: { datasets: response_datasets },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Request Response Time'
      },
    },
  }
});

//
let completedRequestCtx = document.getElementById('CompletedResponse').getContext('2d');
new Chart(completedRequestCtx, {
  type: 'line',
  data: { datasets: completedRequestsLabelsDatasets },
  options: {
    scales: {
      xAxes: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Completed Requests'
      }
    },
  }
});


//
let sentTimeCtx = document.getElementById('SentTime').getContext('2d');
new Chart(sentTimeCtx, {
  type: 'line',
  data: { datasets: sentTime_datasets },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Data Timed Sending'
      },
    },
  }
});

//
let waitTimeCtx = document.getElementById('WaitTime').getContext('2d');
new Chart(waitTimeCtx, {
  type: 'line',
  data: { datasets: waitTime_datasets },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Data Wait Times'
      },
    },
  }
});

//
let receivedTimeCtx = document.getElementById('ReceivedTime').getContext('2d');
new Chart(receivedTimeCtx, {
  type: 'line',
  data: { datasets: receivedTime_datasets },
  options: {
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Data Timed Receiving'
      },
    },
  }
});

</script>
";

            //
            string htmlReport = $@"
<html>
<head>
<script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.5.0/chart.min.js' integrity='sha512-asxKqQghC1oBShyhiBwA+YgotaSYKxGP1rcSYTDrB0U6DxwlJjU59B67U8+5/++uFjcuVM8Hh5cokLjZlhm3Vg==' crossorigin='anonymous' referrerpolicy='no-referrer'></script>
{sourceData}
{responseTime_datasets}
{completedRequest_datasets}
{sentTime_datasets}
{waitTime_datasets}
{receivedTime_datasets}
{sentBytesDataset}
{receivedBytesDataset}
</head>
<body>
<canvas id='ResponseTime' height='70px'></canvas>
<canvas id='CompletedResponse' height='70px'></canvas>
<canvas id='SentTime' height='70px'></canvas>
<canvas id='WaitTime' height='70px'></canvas>
<canvas id='ReceivedTime' height='70px'></canvas>
<canvas id='Sent Bytes' height='70px'></canvas>
<canvas id='Received Bytes' height='70px'></canvas>
{drawGraphs}
{sentBytesChart}
{receivedBytesChart}
</body>
</html>
";
            reportWriter.WriteLine(htmlReport);
            reportWriter.Close();

            
        }
    }

    public static class DateTimeExtension
    {
        public static DateTime Trim(this DateTime dateTime)
        {
            return new(dateTime.Year, 
                dateTime.Month, 
                dateTime.Day, 
                dateTime.Hour, 
                dateTime.Minute, 
                dateTime.Second);
        }
    }
}
