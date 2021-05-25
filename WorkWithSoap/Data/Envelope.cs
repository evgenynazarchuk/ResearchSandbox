using System.Xml;
using System.Xml.Serialization;

namespace WorkWithSoap
{
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }

    public class Header
    {
    }

    public class Body
    {
        [XmlElement(Namespace = "http://host/servce")]
        public SaveEmployee SaveEmployee { get; set; }
    }

    public class SaveEmployee
    {
        [XmlElement(Namespace = "http://host/servce")]
        public Employee Employee { get; set; }
    }

    public class Employee
    {
        [XmlElement(Namespace = "http://host/servce")]
        public int Id { get; set; }
        [XmlElement(Namespace = "http://host/servce")]
        public string Name { get; set; }
    }
}
