namespace MyPregnancy.Infrastructure.Clients.Models
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "Add", Namespace = "http://tempuri.org/")]
        public Add Add { get; set; }
    }
}
