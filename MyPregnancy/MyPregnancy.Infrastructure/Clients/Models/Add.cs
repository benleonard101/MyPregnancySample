namespace MyPregnancy.Infrastructure.Clients.Models
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Add", Namespace = "http://tempuri.org/")]
    public class Add
    {
        [XmlElement(ElementName = "intA", Namespace = "http://tempuri.org/")]
        public string IntA { get; set; }
        [XmlElement(ElementName = "intB", Namespace = "http://tempuri.org/")]
        public string IntB { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
