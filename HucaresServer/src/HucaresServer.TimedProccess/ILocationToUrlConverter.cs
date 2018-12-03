using System;

namespace HucaresServer.TimedProccess
{
    public interface ILocationToUrlConverter
    {
        string ConvertPathToUrl(string fileName, DateTime currentTime);
    }
}