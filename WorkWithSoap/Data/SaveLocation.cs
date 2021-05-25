using System.Xml;
using System.Xml.Serialization;

namespace WorkWithSoap
{
    [XmlRoot(Namespace = "http://host/facade")]
    public class SaveLocation
    {
        public Location Location { get; set; }
    }

    [XmlRoot(Namespace = "http://host/facade")]
    public class Location
    {
        [XmlElement(Namespace = "http://host/facade")]
        public int Id { get; set; }

        [XmlElement(Namespace = "http://host/facade")]
        public string Name { get; set; }
    }
}
