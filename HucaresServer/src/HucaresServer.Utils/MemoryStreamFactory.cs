using System.IO;

namespace HucaresServer.Utils
{
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        public MemoryStream Create(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public MemoryStream Create()
        {
            return new MemoryStream();
        }
    }
}
