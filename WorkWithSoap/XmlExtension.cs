using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WorkWithSoap
{
    public static class XmlExtension
    {
        public static string ToXmlString<T>(this T value, Dictionary<string, string> namespaces = null)
            where T : class, new()
        {
            XmlSerializerNamespaces ns = new();
            if (namespaces is not null)
            {
                foreach ((var prefix, var @namespace) in namespaces)
                {
                    ns.Add(prefix, @namespace);
                }
            }

            var xmlSettings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true, // delete <?xml ... ?>
                CheckCharacters = false,
                Encoding = Encoding.UTF8
            };

            using var stringWriter = new StringWriter();
            using var writer = XmlWriter.Create(stringWriter, xmlSettings);

            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, value, ns);

            return stringWriter.ToString();
        }
    }
}
