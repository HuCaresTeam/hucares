using System.IO;

namespace HucaresServer.Utils
{
    public interface IMemoryStreamFactory
    {
        MemoryStream Create();
        MemoryStream Create(byte[] bytes);
    }
}