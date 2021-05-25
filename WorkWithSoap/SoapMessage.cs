using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace WorkWithSoap
{
    public class SoapMessage
    {
        public static string CreateMessage<TMethod>(TMethod method, string prefix, string @namespace)
            where TMethod : class, new()
        {
            var xmlSettings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
                CheckCharacters = false,
                Encoding = Encoding.UTF8
            };

            using var stringWriter = new StringWriter();
            using var writer = XmlWriter.Create(stringWriter, xmlSettings);

            var namespaces = new Dictionary<string, string>()
            {
                { prefix, @namespace }
            };

            var stream = new StringReader(method.ToXmlString(namespaces));
            var xmlReader = XmlReader.Create(stream);

            writer.WriteStartDocument();

            writer.WriteStartElement(prefix: "soapenv", localName: "Envelope", ns: "http://schemas.xmlsoap.org/soap/envelope/");

            writer.WriteStartElement(prefix: "soapenv", localName: "Header", ns: "http://schemas.xmlsoap.org/soap/envelope/");
            writer.WriteEndElement();

            writer.WriteStartElement(prefix: "soapenv", localName: "Body", ns: "http://schemas.xmlsoap.org/soap/envelope/");

            writer.WriteNode(xmlReader, false);

            writer.WriteEndElement(); // Body
            writer.WriteEndElement(); // Envelope
            writer.WriteEndDocument();
            writer.Flush(); // Save!!!

            return stringWriter.ToString();
        }
    }
}
