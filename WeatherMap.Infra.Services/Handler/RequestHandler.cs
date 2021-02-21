using RestSharp;
using System.Collections.Generic;

namespace WeatherMap.Infra.Services.Handler
{
    public class RequestHandler
    {
        public string Get(string clientUrl, string requestUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest(requestUrl, Method.GET);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
