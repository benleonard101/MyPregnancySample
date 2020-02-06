namespace MyPregnancy.Infrastructure.Clients
{
    using MyPregnancy.TaxCalculators.Interfaces;
    using RestSharp;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using MyPregnancy.Infrastructure.Clients.Models;
    using System.Xml;
    using System.IO;
    using System.Xml.Serialization;
    using Microsoft.Extensions.Logging;

    public class CalculatorClient : BaseApiClient, ICalculatorClient
    {
        private const string SoapAction = "SOAPAction";
        private const string AddOperation = "http://tempuri.org/Add";
        private const string XmlUtf8 = "text/xml; charset=utf-8";
        private const string ContentType = "Content-Type";
        private const string SoapBody = "soap:Body";

        private readonly ILogger _logger;

        public CalculatorClient(IOptions<CalculatorClientConfiguration> appSettings, ILogger<CalculatorClient> logger)
            : base(appSettings.Value.CalculatorClientUrl)
        {
            _logger = logger;
        }

        public async Task<int> Add(int a, int b)
        {
            _logger.LogInformation($"Entering {nameof(Add)}");

            var request = new RestRequest(Method.POST);

            request.AddHeader(SoapAction, AddOperation);
            request.AddHeader(ContentType, XmlUtf8);

            var envelope = new Envelope
            {
                Body = new Body
                {
                    Add = new Add
                    {
                        IntA = a.ToString(),
                        IntB = b.ToString()
                    }
                }
            };

            request.AddXmlBody(envelope);

            var response = await Execute(request);

            return ExtractAddResult(response.Content);
        }

        private int ExtractAddResult(string content)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var soapBody = xmlDocument.GetElementsByTagName(SoapBody)[0];
            string innerObject = soapBody.InnerXml;

            XmlSerializer deserializer = new XmlSerializer(typeof(AddResponse));

            AddResponse addResponse;

            using (StringReader reader = new StringReader(innerObject))
            {
                addResponse = (AddResponse)deserializer.Deserialize(reader);
            }

            return int.Parse(addResponse.AddResult);
        }
    }
}
