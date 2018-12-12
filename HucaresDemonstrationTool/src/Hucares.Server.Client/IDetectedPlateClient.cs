using System;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public interface IDetectedPlateClient
    {
        Uri HostUri { get; }

        Task DeleteAllDLPs();
    }
}