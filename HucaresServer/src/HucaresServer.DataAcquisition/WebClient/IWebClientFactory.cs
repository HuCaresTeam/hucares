using System.Net;

namespace HucaresServer.DataAcquisition
{
    public interface IWebClientFactory
    {
        IWebClient BuildWebClient();
    }
}