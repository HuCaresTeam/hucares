using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage
{
    public class DbContextFactory : IDbContextFactory
    {
        public HucaresContext BuildHucaresContext()
        {
            return new HucaresContext();
        }
    }
}
