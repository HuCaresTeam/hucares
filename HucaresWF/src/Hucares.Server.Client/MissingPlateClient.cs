using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;
using Newtonsoft.Json;

namespace Hucares.Server.Client
{
    public class MissingPlateClient : IMissingPlateClient
    {
        private Uri HostUri { get; set; } = new Uri("http://localhost:50510");
        
        private readonly HttpClientHelper httpHelper;
        
        public MissingPlateClient(HttpClientHelper httpHelper = null)
        {
            this.httpHelper = httpHelper ?? new HttpClientHelper();
        }
        
        public async Task<MissingLicensePlate> InsertPlateRecord(string plateNumber, DateTime searchStartDatetime)
        {
            var uri = $"api/mlp/insert";
            var fullUri = new Uri(HostUri, uri);

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

            return await httpHelper.MakeRequest<MissingLicensePlate>(request);
        }

        public async Task<IEnumerable<MissingLicensePlate>> GetAllPlateRecords()
        {
            var uri = $"api/mlp/all";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            
            return await httpHelper.MakeRequest<IEnumerable<MissingLicensePlate>>(request);
        }

        public async Task<IEnumerable<MissingLicensePlate>> GetPlateRecordByPlateNumber(string plateNumber)
        {
            var uri = $"api/mlp/plate/{plateNumber}";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            
            return await httpHelper.MakeRequest<IEnumerable<MissingLicensePlate>>(request);
        }

        public async Task<MissingLicensePlate> UpdatePlateRecord(int plateId, string plateNumber, DateTime searchStartDatetime)
        {
            var uri = $"api/mlp/update/{plateId}";
            var fullUri = new Uri(HostUri, uri);

            var plateNumberData = new
            {
                plateId = plateId,
                plateNumber = plateNumber,
                searchStartDatetime = searchStartDatetime
            };
            
            var jsonContent = JsonConvert.SerializeObject(plateNumberData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            
            var request = new HttpRequestMessage(HttpMethod.Put, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<MissingLicensePlate>(request);
        }

        public async Task<MissingLicensePlate> MarkFoundPlate(int plateId, DateTime requestDateTime, bool isFound)
        {
            var uri = $"api/mlp/found/{plateId}";
            var fullUri = new Uri(HostUri, uri);
            
            var plateNumberData = new
            {
                requestDateTime = requestDateTime,
                isFound = isFound
            };

            var jsonContent = JsonConvert.SerializeObject(plateNumberData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<MissingLicensePlate>(request);
        }

        public async Task<MissingLicensePlate> DeletePlateById(int plateId)
        {
            var uri = $"api/mlp/delete/{plateId}";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);

            return await httpHelper.MakeRequest<MissingLicensePlate>(request);
        }

        public async Task<MissingLicensePlate> DeletePlateByNumber(string plateNumber)
        {
            var uri = $"api/mlp/delete/{plateNumber}";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Delete, fullUri);
                
            return await httpHelper.MakeRequest<MissingLicensePlate>(request);
        }
    }
}
