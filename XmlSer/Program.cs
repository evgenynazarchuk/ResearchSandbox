using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlSer
{
    class Program
    {
        static void Main(string[] args)
        {
            var locationSer = new XmlSerializer(typeof(Location));

            var location = new Location
            {
                Id = 1,
                Name = "Test Location"
            };

            using var stringWriter = new StringWriter();
            locationSer.Serialize(stringWriter, location);
            Console.WriteLine(stringWriter.ToString());

            using var stringReader = new StringReader(stringWriter.ToString());

            var obj = locationSer.Deserialize(stringReader) as Location;
            Console.WriteLine($"Location: {obj}");
        }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
