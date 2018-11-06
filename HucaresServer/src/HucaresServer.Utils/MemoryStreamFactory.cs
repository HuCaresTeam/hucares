using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
