using System.Net;

namespace HucaresServer.DataAcquisition
{
    public interface IWebClientFactory
    {
        WebClient BuildWebClient();
    }
}