using System.Net;

namespace HucaresServer.DataAcquisition
{
    public class CustomWebClientFactory : IWebClientFactory
    {
        public IWebClient BuildWebClient()
        {
            return new CustomWebClient();
        }
    }
}