using System.Net;

namespace HucaresServer.DataAcquisition
{
    public class CustomWebClientFactory : IWebClientFactory
    {
        public WebClient BuildWebClient()
        {
            return new WebClient();
        }
    }
}