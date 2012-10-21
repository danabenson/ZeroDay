using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace NatGeoMetroApp.Common
{
    public static class StringExtensions
    {
        public static T FromXml<T>(this string xmlString)
        {
            var serializer = new XmlSerializer(typeof (T));
            using (var stringReader = new StringReader(xmlString))
            {
                var obj = (T)serializer.Deserialize(stringReader);
                return obj;
            }
        }

        public static T FromJson<T>(this string json)
        {
            var _Bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream _Stream = new MemoryStream(_Bytes))
            {
                var _Serializer = new DataContractJsonSerializer(typeof(T));
                return (T)_Serializer.ReadObject(_Stream);
            }
        }
    }
}