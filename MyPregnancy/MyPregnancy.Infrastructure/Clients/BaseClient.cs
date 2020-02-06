namespace MyPregnancy.Infrastructure.Clients
{
    using RestSharp;
    using RestSharp.Serialization.Xml;
    using System;
    using System.Threading.Tasks;

    public class BaseApiClient
    {
        private const string Errormessage = "Error retrieving response. Check inner details for more info.";
        private readonly IRestClient _client;

        public BaseApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
            _client.UseDotNetXmlSerializer();
        }

        protected async Task<IRestResponse> Execute(RestRequest request)
        {
            var response = await _client.ExecuteTaskAsync(request);

            if (response.ErrorException != null)
            {
                var applicationException = new ApplicationException(Errormessage, response.ErrorException);
                throw applicationException;
            }

            return response;
        }
    }
}
