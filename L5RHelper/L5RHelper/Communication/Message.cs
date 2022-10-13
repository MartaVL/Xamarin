using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;

namespace L5RHelper
{
    [XmlRoot(ElementName = "L5RHelper")]
    public class Message
    {
        [XmlElement(ElementName = "dice")]
        public List<Die> Dice { get; set; }

        public override string ToString()
        {
            using(var stringWriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stringWriter, this);
                return stringWriter.ToString();

            }
        }

        public static Message ToObject(string xmlMessageRoll)
        {
            using(var stringReader = new System.IO.StringReader(xmlMessageRoll))
            {
                var serializer = new XmlSerializer(typeof(Message));
                return serializer.Deserialize(stringReader) as Message;
            }
        }
    }
}
