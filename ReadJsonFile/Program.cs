using System;
using System.IO;
using System.Text.Json;
using System.Text;

namespace ReadJsonFile
{
    class Program
    {
        static void Main()
        {
            ReadJson();
        }

        public static void ReadJson()
        {
            const string path = "LocationJsonFile.json";
            var stream = new StreamReader(path);
            string line;

            while ((line = stream.ReadLine()) != null)
            {
                var opt = new JsonSerializerOptions();
                opt.PropertyNameCaseInsensitive = true; // не учитываем регистр атрибутов

                var obj = JsonSerializer.Deserialize<LocationDto>(line, opt);
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(obj);
            }

            stream.Close();
        }
    }
}
