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
                .GroupBy(x => new { x.User, x.Request, x.StatusCode, EndResponse = (long)(x.EndResponse / 10000000) })
                .Select(x => new
                {
                    x.Key,
                    CompletedRequest = x.LongCount(),
                    SentBytes = x.Sum(y => y.SendBytes),
                    ReceivedBytes = x.Sum(y => y.ReceiveBytes),
                    ResponseTime = x.Average(y => y.EndResponse - y.StartSendRequest),
                    SentTime = x.Average(y => y.StartWaitResponse - y.StartSendRequest),
                    WaitTime = x.Average(y => y.StartResponse - y.StartWaitResponse),
                    ReceivedTime = x.Average(y => y.EndResponse - y.StartResponse)
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
                .GroupBy(x => new { EndResponse = x.EndResponse / 10000000 })
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
const groupedRawLog = [{groupedStringLog}]
const sentBytesLog = [{sentStringLog}]
const receivedBytesLog = [{receivedStringLog}]
</script>

";

            //
            var axisFontLayout = @"
<script>
let yaxisFontLayout = {
	family: 'Arial',
	size: 16,
	color: '#7f7f7f'
}		
let xaxisFontLayout = {
	family: 'Arial',
	size: 16,
	color: '#7f7f7f'
}
</script>
";

			//
			var titleFontLayout = @"
<script>
let titleFontLayout = {
	family: 'Arial',
	size: 32
}
</script>
";

            //
            var responseTimeChart = @"
<script>
let responseTimeData = {}
for(let item of groupedRawLog) {
	if (responseTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        responseTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
	let date = new Date(0);
	date.setSeconds(item.EndResponse);
	let timeString = date.toISOString().substr(11, 8);
    responseTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: timeString, y: item.ResponseTime / 10000 })
}

let responseTimeChartDatasets = []
for(let key in responseTimeData) {
	responseTimeChartDatasets.push({
		x: responseTimeData[key].map(item => item.x),
		y: responseTimeData[key].map(item => item.y),
		type: 'scatter'
	})
}

let responseTimeChartLayout ={
	title: {
		text:'Response Time',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
		text: '',
		font: xaxisFontLayout
		}
	},
	
	yaxis: {
		title: {
		text: 'Milliseconds',
		font: yaxisFontLayout
		}
	}
}


Plotly.newPlot('ResponseTimeChart', responseTimeChartDatasets, responseTimeChartLayout);
</script>
";

            //
            var completedRequestsChart = @"
<script>
let completedRequestsData = { };
for (let item of groupedRawLog)
{
    if (completedRequestsData[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        completedRequestsData[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
	let date = new Date(0);
	date.setSeconds(item.EndResponse);
	let timeString = date.toISOString().substr(11, 8);
    completedRequestsData[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: timeString, y: item.CompletedRequest })
}

let completedRequestsChartDatasets = []
for(let key in completedRequestsData) {
	completedRequestsChartDatasets.push({
		x: completedRequestsData[key].map(item => item.x),
		y: completedRequestsData[key].map(item => item.y),
		type: 'scatter'
	})
}

let completedRequestsChartLayout = {
	title: {
		text:'Completed Requests',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
			text: '',
			font: xaxisFontLayout
		}
	},
	yaxis: {
		title: {
			text: 'Requests',
			font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('CompletedRequestsChart', completedRequestsChartDatasets, completedRequestsChartLayout);
</script>
";

            //
            var sentTimeChart = @"
<script>
let sentTimeData = { };
for (let item of groupedRawLog)
{
    if (sentTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        sentTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
	let date = new Date(0);
	date.setSeconds(item.EndResponse);
	let timeString = date.toISOString().substr(11, 8);
    sentTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: timeString, y: item.SentTime / 10000 })
}

let sentTimeChartDatasets = []
for(let key in sentTimeData) {
	sentTimeChartDatasets.push({
		x: sentTimeData[key].map(item => item.x),
		y: sentTimeData[key].map(item => item.y),
		type: 'scatter'
	})
}


let sentTimeChartlayout ={
	title: {
		text:'Data Timed Sending',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
			text: '',
			font: xaxisFontLayout
		}
	},
	yaxis: {
		title: {
			text: 'Milliseconds',
			font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('SentTimeChart', sentTimeChartDatasets, sentTimeChartlayout);
</script>
";

            //
            var waitTimeChart = @"
<script>
let waitTimeData = { };
for (let item of groupedRawLog)
{
    if (waitTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        waitTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
	let date = new Date(0);
	date.setSeconds(item.EndResponse);
	let timeString = date.toISOString().substr(11, 8);
    waitTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: timeString, y: item.WaitTime / 10000 })
}

let waitTimeChartDatasets = []
for(let key in waitTimeData) {
	waitTimeChartDatasets.push({
		x: waitTimeData[key].map(item => item.x),
		y: waitTimeData[key].map(item => item.y),
		type: 'scatter'
	})
}

let waitTimeChartLayout ={
	title: {
		text:'Data Wait Times',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
			text: '',
			font: xaxisFontLayout
		}
	},
	yaxis: {
		title: {
			text: 'Milliseconds',
			font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('WaitTimeChart', waitTimeChartDatasets, waitTimeChartLayout);
</script>
";

            //
            var receivedTimeChart = @"
<script>
let receivedTimeData = { };
for (let item of groupedRawLog)
{
    if (receivedTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] == undefined)
    {
        receivedTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode] = []
    }
	let date = new Date(0);
	date.setSeconds(item.EndResponse);
	let timeString = date.toISOString().substr(11, 8);
    receivedTimeData[item.User + ' ' + item.Request + ' ' + item.StatusCode].push({ x: timeString, y: item.ReceivedTime / 10000 })
}

let receivedTimeChartDatasets = []
for(let key in receivedTimeData) {
	receivedTimeChartDatasets.push({
		x: receivedTimeData[key].map(item => item.x),
		y: receivedTimeData[key].map(item => item.y),
		type: 'scatter'
	})
}

let receivedTimeChartLayout ={
	title: {
		text:'Data Timed Receiving',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
		text: '',
		font: xaxisFontLayout
		}
	},
	
	yaxis: {
		title: {
		text: 'Milliseconds',
		font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('ReceivedTimeChart', receivedTimeChartDatasets, receivedTimeChartLayout);
</script>
";

			//
			var sentBytesChart = @"
<script>
let sentBytesChartDataset = []
sentBytesChartDataset.push({
	x: sentBytesLog.map(item => {
		let date = new Date(0);
		date.setSeconds(item.EndResponse);
		return date.toISOString().substr(11, 8);
	}),
	y: sentBytesLog.map(item => item.Count),
	type: 'scatter'
})

let sentBytesChartLayout ={
	title: {
		text:'Sent Bytes',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
		text: '',
		font: xaxisFontLayout
		}
	},
	
	yaxis: {
		title: {
		text: 'Bytes',
		font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('SentBytesChart', sentBytesChartDataset, sentBytesChartLayout);
</script>
";

			//
			var receivedBytesChart = @"
<script>
let receivedBytesChartDataset = []
receivedBytesChartDataset.push({
	x: receivedBytesLog.map(item => {
		let date = new Date(0);
		date.setSeconds(item.EndResponse);
		return date.toISOString().substr(11, 8);
	}),
	y: receivedBytesLog.map(item => item.Count),
	type: 'scatter'
})

let receivedBytesChartLayout ={
	title: {
		text:'Received Bytes',
		font: titleFontLayout,
	},
	xaxis: {
		title: {
		text: '',
		font: xaxisFontLayout
		}
	},
	
	yaxis: {
		title: {
		text: 'Bytes',
		font: yaxisFontLayout
		}
	}
}

Plotly.newPlot('ReceivedBytesChart', receivedBytesChartDataset, receivedBytesChartLayout);
</script>
";

            //
            string htmlReport = $@"
<html>
<head>
<script src='https://cdn.plot.ly/plotly-2.3.0.min.js'></script>
</head>
<body>
<div id='ResponseTimeChart' style='width:99%;height:400px;'></div>
<div id='CompletedRequestsChart' style='width:99%;height:400px;'></div>
<div id='SentTimeChart' style='width:99%;height:400px;'></div>
<div id='WaitTimeChart' style='width:99%;height:400px;'></div>
<div id='ReceivedTimeChart' style='width:99%;height:400px;'></div>
<div id='SentBytesChart' style='width:99%;height:400px;'></div>
<div id='ReceivedBytesChart' style='width:99%;height:400px;'></div>
{sourceData}
{axisFontLayout}
{titleFontLayout}
{responseTimeChart}
{completedRequestsChart}
{sentTimeChart}
{waitTimeChart}
{receivedTimeChart}
{sentBytesChart}
{receivedBytesChart}
</body>
</html>
";

            

            //
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
