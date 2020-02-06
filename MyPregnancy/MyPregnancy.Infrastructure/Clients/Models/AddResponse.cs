namespace MyPregnancy.Infrastructure.Clients.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "AddResponse", Namespace = "http://tempuri.org/")]
    public class AddResponse
    {
        [XmlElement(ElementName = "AddResult", Namespace = "http://tempuri.org/")]
        public string AddResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
