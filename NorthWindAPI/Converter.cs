using Newtonsoft.Json;
using NorthWindAPI.Models;
using System.Xml;
using System.Xml.Serialization;

namespace NorthWindAPI
{
    static public class Converter
    {
        private static XmlAttributeOverrides _attrOverrides = new XmlAttributeOverrides();
        private static XmlAttributes _attrs = new XmlAttributes { XmlIgnore = true };
        private static StringWriter _stringwriter = new StringWriter();
        private static XmlWriterSettings _settings = new XmlWriterSettings() { Indent = true };
        static public string ToXml<T>(T obj)
        {
            if(obj is List<Customer> || obj is Customer)
                _attrOverrides.Add(typeof(Customer), "Orders", _attrs);

            XmlSerializer serializer = new XmlSerializer(obj.GetType(), _attrOverrides);
            XmlWriter writer = XmlWriter.Create(_stringwriter, _settings);
            serializer.Serialize(writer, obj);
            return _stringwriter.ToString();
        }

        static public string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
