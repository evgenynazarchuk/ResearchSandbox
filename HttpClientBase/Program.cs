using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HttpClientBase
{
    class Program
    {
        static void Main(string[] args)
        {
            // Что мне нжно?
            // Set base Address +
            // Set Https or Http
            // Set Http Version
            // Set Timeout
            // Add Header ?
            // Add Url Parametrs
            // Add Body ?
            // Recieve Body ?

            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            var endPoint = "posts";
            var targetUrl = new Uri(httpClient.BaseAddress, endPoint);
            var httpMethod = HttpMethod.Get; // Или new HttpMethod("GET");

            var countRequest = 2;
            var requestTasks = new Task[countRequest];
            for (int i = 0; i < countRequest; i++)
            {
                // Для каждого запроса нужно создавать свой HttpRequestMessage
                var httpMessage = new HttpRequestMessage(httpMethod, targetUrl);
                httpMessage.RequestUri = targetUrl;
                httpMessage.Version = HttpVersion.Version20;

                requestTasks[i] = Request(endPoint, httpClient, httpMessage);
            }
            Task.WaitAll(requestTasks);
        }

        public static async Task Request(string requestName, HttpClient httpClient, HttpRequestMessage httpMessage)
        {
            //var watch = new Stopwatch();

            var startTime = DateTime.Now;
            //watch.Start();
            var response = await httpClient.SendAsync(httpMessage);
            //watch.Stop();
            var endTime = DateTime.Now;

            //Console.WriteLine(watch.ElapsedMilliseconds);
            Console.WriteLine("HTTP Version: " + response.Version);

            TimeSpan diff = endTime.Subtract(startTime);

            // Результаты в одном WriteLine, т.к. в многопоточности вызовы Write могут пересекаться
            var result = $"{requestName} {response.StatusCode} {startTime.ToString("O")}\t" +
                $"{endTime.ToString("O")}\t" +
                $"{diff}";

            Console.WriteLine(result);
        }
    }
}
