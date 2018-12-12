using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public class DetectedPlateClient : IDetectedPlateClient
    {
        public Uri HostUri { get; private set; } = new Uri("http://localhost:50510");
        private readonly HttpClientHelper httpHelper;

        public DetectedPlateClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }
        
        public async Task DeleteAllDLPs()
        {
            var uri = "api/dlp/all";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);

            await httpHelper.MakeRequest(request);
        }
    }
}