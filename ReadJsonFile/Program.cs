using System;
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
            const string path = "location.json";
            var jsonReader = new JsonReader<Location>(path);
            Location location;

            Console.OutputEncoding = Encoding.UTF8;
            while ((location = jsonReader.GetNextObject()) != null)
            {
                Console.WriteLine(location);
            }
        }
    }
}
