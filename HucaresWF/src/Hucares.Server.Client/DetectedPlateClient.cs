using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hucares.Server.Client.Models;
using Newtonsoft.Json;

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
        
        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedMissingPlates()
        {
            var uri = "api/dlp/all";
            var fullUri = new Uri(HostUri, uri);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }

        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByPlateNumber(string plateNumber, DateTime? startDateTime = null,
            DateTime? endDateTime = null)
        {
            var uri = $"api/dlp/plate/{plateNumber}";
            var fullUri = new Uri(HostUri, uri);

            var plateNumberData = new
            {
                plateNumber = plateNumber,
                startDateTime = startDateTime,
                endDateTime = endDateTime
            };

            var jsonContent = JsonConvert.SerializeObject(plateNumberData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }

        public async Task<IEnumerable<DetectedLicensePlate>> GetAllDetectedPlatesByCamera(int cameraId, DateTime? startDateTime = null, 
			DateTime? endDateTime = null)
        {
            var uri = $"api/dlp/cam/{cameraId}";
            var fullUri = new Uri(HostUri, uri);

            var cameraIdData = new
            {
                cameraId = cameraId,
                startDateTime = startDateTime,
                endDateTime = endDateTime
            };

            var jsonContent = JsonConvert.SerializeObject(cameraIdData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            
            var request = new HttpRequestMessage(HttpMethod.Post, fullUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            return await httpHelper.MakeRequest<IEnumerable<DetectedLicensePlate>>(request);
        }
    }
}