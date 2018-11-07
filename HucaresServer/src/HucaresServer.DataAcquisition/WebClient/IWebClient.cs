using System;

namespace HucaresServer.DataAcquisition
{
    public interface IWebClient : IDisposable
    {
        byte[] DownloadData(string url);
    }
}