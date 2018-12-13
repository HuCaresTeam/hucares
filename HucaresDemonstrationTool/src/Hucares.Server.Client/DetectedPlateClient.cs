using Newtonsoft.Json;
using SqliteManipulation.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hucares.Server.Client
{
    public class DetectedPlateClient : IDetectedPlateClient
    {
        private readonly HttpClientHelper httpHelper;

        public DetectedPlateClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }
        
        public async Task DeleteAllDLPs()
        {
            var uri = "api/dlp/all";
            var fullUri = new Uri(httpHelper.HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);

            await httpHelper.MakeRequest(request);
        }

        public async Task DemonstrationAddDlp(DLP dlp)
        {
            var uri = "api/dlp/demonstration";
            var fullUri = new Uri(httpHelper.HostUri, uri);

            var jsonContent = JsonConvert.SerializeObject(dlp, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            await httpHelper.MakeRequest<DLP>(request);
        }
    }
}