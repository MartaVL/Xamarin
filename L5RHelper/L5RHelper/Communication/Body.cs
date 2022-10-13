using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace L5RHelper
{
    [XmlRoot(ElementName = "body")]
    public class Body
    {
       

        [XmlElement(ElementName = "master", IsNullable = true)]
        public string Master { get; set; }
    }
}
