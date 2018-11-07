using OpenAlprApi.Model;
using System.Threading.Tasks;

namespace HucaresServer.DataAcquisition
{
    public interface IOpenAlprWrapper
    {
        Task<InlineResponse200> DetectPlateAsync(string pathToPlateImage);
    }
}