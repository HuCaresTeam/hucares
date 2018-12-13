using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SqliteManipulation.Models;

namespace Hucares.Server.Client
{
    public class MissingPlateClient : IMissingPlateClient
    {        
        private readonly HttpClientHelper httpHelper;
        
        public MissingPlateClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }
        
        public async Task<MLP> InsertPlateRecord(string plateNumber, string searchStartDatetime)
        {
            var uri = $"api/mlp/insert";
            var fullUri = new Uri(httpHelper.HostUri, uri);

            var plateNumberData = new
            {
                plateNumber = plateNumber,
                searchStartDatetime = searchStartDatetime
            };

            var jsonContent = JsonConvert.SerializeObject(plateNumberData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<MLP>(request);
        }

        public async Task DeleteAllMLPs ()
        {
            var uri = $"api/mlp/all";
            var fullUri = new Uri(httpHelper.HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);
            
            await httpHelper.MakeRequest(request);
        }
    }
}
