using System.IO;

namespace HucaresServer.Utils
{
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        public MemoryStream Create()
        {
            return new MemoryStream();
        }
    }
}
