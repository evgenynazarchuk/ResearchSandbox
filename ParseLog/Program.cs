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
            StreamReader reader = new("result.log", Encoding.UTF8, false, 65535);
            List<LogMessage> logMessages = new();

            string? logString;
            LogMessage? logMessage;
            while ((logString = reader.ReadLine()) != null)
            {
                logMessage = JsonSerializer.Deserialize<LogMessage>(logString);
                if (logMessage is null) break;
                logMessages.Add(logMessage);
            }
            reader.Close();

            var groupByRequestAndDateTime = logMessages
                .GroupBy(x => new { x.Request, x.StatusCode, EndResponse = x.EndResponse.Trim() })
                .Select(x => new
                {
                    x.Key,
                    CompletedRequest = x.LongCount(),
                    SentBytes = x.Sum(y => y.SendBytes),
                    ReceivedBytes = x.Sum(y => y.ReceiveBytes),
                    ResponseTime = x.Average(y => y.EndResponse.Subtract(y.StartSendRequest).Ticks)
                });

            StreamWriter writer = new("total.html", false, Encoding.UTF8, 65355);
            StringBuilder builder = new();

            foreach (var item in groupByRequestAndDateTime)
            {
                TotalLog totalLog = new(item.Key.Request,
                    item.Key.StatusCode,
                    item.Key.EndResponse,
                    item.CompletedRequest,
                    item.ResponseTime,
                    item.SentBytes,
                    item.ReceivedBytes);
            
                builder.Append(JsonSerializer.Serialize(totalLog) + ",\n");
            }

            string completedRequest_datasets = @"
<script>
let labels = { };
for (let item of data)
{
    if (labels[item.Request + ' ' + item.StatusCode] == undefined)
    {
        labels[item.Request + ' ' + item.StatusCode] = []
    }
    labels[item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.CompletedRequest })
}

let datasets = []
for(let key in labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = labels[key]
    datasets.push(line)
}
</script>
";

            string responseTime_datasets = @"
<script>
let response_labels = { };

for (let item of data)
{
    if (response_labels[item.Request + ' ' + item.StatusCode] == undefined)
    {
        response_labels[item.Request + ' ' + item.StatusCode] = []
    }
    response_labels[item.Request + ' ' + item.StatusCode].push({ x: item.EndResponse, y: item.ResponseTime / 1000 })
}

let response_datasets = []
for(let key in response_labels)
{
    let line = {}
    line.label = key
    line.borderColor = 'rgb(54, 162, 235, 0.7)'
    line.backgroundColor = 'transparent'
    line.data = response_labels[key]
    response_datasets.push(line)
}
</script>
";

            string drawGraphs = @"
<script>
let completedRequestCtx = document.getElementById('CompletedResponse').getContext('2d');
new Chart(completedRequestCtx, {
  type: 'line',
  data: { datasets: datasets },
  options: {
	responsive: true,
    scales: {
      xAxes: [{
        type: 'time',		
		distribution: 'linear'
      }],
      yAxes: {
                min: 400,
                max: 1200,
				
      }
    },
	plugins: {
      title: {
        display: true,
        text: 'Completed Request per second'
      }
    },
  }
});

let responseTimeCtx = document.getElementById('ResponseTime').getContext('2d');
new Chart(responseTimeCtx, {
  type: 'line',
  data: { datasets: response_datasets },
  options: {
  responsive: true,
    scales: {
      x: [{
        type: 'time',
      }],
    },
	plugins: {
      title: {
        display: true,
        text: 'Response Time'
      },
    },
  }
});
</script>
";

            string html = $@"
<html>
<head>
<script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.5.0/chart.min.js' integrity='sha512-asxKqQghC1oBShyhiBwA+YgotaSYKxGP1rcSYTDrB0U6DxwlJjU59B67U8+5/++uFjcuVM8Hh5cokLjZlhm3Vg==' crossorigin='anonymous' referrerpolicy='no-referrer'></script>
<script>
const data = [{builder}]
</script>
{completedRequest_datasets}
{responseTime_datasets}
</head>
<body style='background-color: black'>
<canvas id='CompletedResponse' height='70px'></canvas>
<canvas id='ResponseTime' height='70px'></canvas>
{ drawGraphs}
</body>
</html>
";
            writer.WriteLine(html);
            writer.Close();

            
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
